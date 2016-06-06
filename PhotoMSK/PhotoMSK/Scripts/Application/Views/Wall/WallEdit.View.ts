/// <reference path="../../../typings/jqueryui/jqueryui.d.ts" />

module App.Views {
    import WallCollection = Collections.WallCollection;
    import WallModel = Models.WallModel;

    export class WallEditView extends Backbone.View<WallModel>{
        constructor(model?) {
            super(model);
            this.template = Tools.getTemplate("#wall-edit-view-template");
            this.autoItem = Tools.getTemplate("#autocomplete-item-view-template");
            this.partsItem = Tools.getTemplate("#wall-participant-view-template");

            this.events = {
                "click .role-save-wall-edit": "saveWall",
                "click .role-shooting-edit": "shooting",
                "click .role-post-edit": "posting",
                "focus .role-wall-description": "showSave",
                "blur .role-wall-description": "descriptionChanged",
                "keyup .role-shooting-autocomplete": "addGenre",
                "change .role-wall-video": "addVideo",
                "click .role-toggle-video": "toggleVideo"
            };

            this.participants = [];
            this.attachments = [];
            this.genres = [];
            this.isshooting = false;
        }
        
        template: (...data: any[]) => string;
        autoItem: (...data: any[]) => string;
        partsItem: (...data: any[]) => string;
        wallCollection: WallCollection;

        progressbar: JQuery;
        bar: JQuery;
        
        participants: any;
        attachments: any;
        genres: any;
        isshooting: any;

        initialize(options) {
            this.wallCollection = options.WallCollection;
        }
        render() {
            this.setElement(this.template());

            var self = this;

            var auto = this.$el.find("#routes-autocomplete").autocomplete({
                appendTo: this.$el.find(".role-wall-participant-autocomplete"),
                source: (request, respond) => {
                    $.get(App.makeUrl("/api/Search/"), { search: request.term },
                        response => {
                            respond(response);
                        });
                },
                minLength: 2,
                select: (event, ui) => {
                    this.selectItem(ui.item);
                }
            });


            

            auto.data("ui-autocomplete")._renderItem = (ul, item) => this.renderAutocompleteItem(ul, item);

            $(".ui-autocomplete").attr("class", "uk-nav uk-nav-autocomplete uk-autocomplete-results");

            var genres = this.$(".role-shooting-autocomplete").autocomplete({
                source: (request, respond) => {
                    $.post("/vjson/Genre/FindGenres", { search: request.term },
                        response => {
                            respond(response);
                        });
                },
                minLength: 2,
                //select: function(event, ui) {
                //    self.selectGenre(ui.item);
                //}
            });

            this.progressbar = this.$el.find("#progressbar");
            this.bar = this.progressbar.find('.uk-progress-bar');
            var settings = {

                action: $('#config-settings').attr('data-upload-url') + "/Api/Files", // upload url
                single: false,
                allow: '*.(jpg|jpeg|gif|png)', // allow only images

                loadstart: function () {
                    self.bar.css("width", "0%").text("0%");
                    self.progressbar.removeClass("uk-hidden");
                },

                progress: function (percent) {
                    percent = Math.ceil(percent);
                    self.bar.css("width", percent + "%").text(percent + "%");
                },

                allcomplete: function (response) {

                    self.filesUpload(response);
                }
            };

            var select = new UIkit.uploadSelect(this.$el.find("#upload-select"), settings);
            var drop = new UIkit.uploadDrop(this.$el.find("#upload-drop"), settings);

            return this;
        }

        saveWall() {
            var self = this;
            var model = new Models.WallModel();
            if (typeof (App.owner) == "undefined")
                throw "owner id must be defined";
            var walldetalis = {
                owner: App.owner,
                title: $(".role-wall-title").val(),
                genres: self.genres,
                shooting: self.isshooting,
                description: $(".role-wall-description").val(),
                routes: self.participants,
                attachments: self.attachments
            }
            model.save(walldetalis, {
                success: function (item, response) {
                    $("#wall-post-text").val("");
                    self.$(".role-wall-title").val('');
                    self.$(".role-wall-description").val('');

                    self.$(".wall-post-participants").html('');
                    self.participants = [];

                    self.$('.role-photo-placeholder').html('');
                    self.$('iframe').attr('src', '').hide();
                    self.attachments = [];

                    self.$('.role-shooting-genres').html('');
                    self.genres = [];

                    self.$('.role-show-when-post-edit').slideToggle();

                    self.wallCollection.add(response);
                }
            });
        }

        renderAutocompleteItem(ul, item) {
            return $(this.autoItem(item)).appendTo(ul);
        }

        selectItem(item) {
            $(this.partsItem(item)).appendTo(".wall-post-participants");
            this.participants.push(item.id);
        }
        selectGenre(item) {
            if (this.genres.length < 3) {
                this.genres.push(item);
                this.$(".role-shooting-genres").append("<div>#" + item + "</div>");
            }
        }
        addVideo(event) {
            this.preview(this.$(".role-wall-video").val());
        }
        videoSaved(model) {

        }
        filesUpload(response) {
            var self = this;

            this.bar.css("width", "100%").text("100%");

            setTimeout(function () {
                self.progressbar.addClass("uk-hidden");
            }, 250);

            var array = eval(response);

            $.each(array, function () {
                var model = new App.Models.AttachmentModel(this);
                model.set("type", "photo");
                self.attachments.push(model);

                $("<img></img>").attr("src", this.url).css("width", "75px").appendTo("#photo-placeholder");
            });
        }
        descriptionChanged(event) {
            this.model.set("description", this.$el.find(".Description").val());
        }
        preview(videoUri) {
            if ((!videoUri.match('http://*')) && (!videoUri.match('https://*'))) {
                return;
            }
            var model = new App.Models.AttachmentModel();
            model.set('type', "video");

            if (videoUri.indexOf("youtu") != -1) {

                var array = videoUri.split("?");
                var array2 = array[1].split("=");

                this.$el.find(".yuotube-video").attr("src", "https://www.youtube.com/embed/" + array2[1]);

                model.set("url", "https://www.youtube.com/embed/" + array2[1]);
                model.set("reference", "https://www.youtube.com/embed/" + array2[1]);

                this.$el.find(".youtube").show();
                this.trigger("added");
            }

            if (videoUri.indexOf("vimeo") != -1) {
                var array = videoUri.split("com/");
                this.$el.find(".vimeo-video").attr("src", " http://player.vimeo.com/video/" + array[1]);
                model.set("url", "http://player.vimeo.com/video/" + array[1]);
                model.set("reference", "http://player.vimeo.com/video/" + array[1]);
                this.$el.find(".vimeo").show();
                this.trigger("added");
            }

            // http://www.youtube.com/watch?v=AE0eTHdHPkQ
            // Specified dimentions are feasible for YouTube video only.
            // TODO: Process the input video URL in order to determine the video hosting and 
            // TODO: change video dimentions according to the video hosting.
            // TODO: For YouTube: width="420" height="315"
            // TODO: For Vimeo: width="500" height="281" 
            // videoUrl accepts URLs such as http://www.youtube.com/embed/yRhesBaRCME only.
            // TODO: Process the input video URL in order to determine the video hosting and 
            // TODO: change src value accordingly.
            // TODO: The following URL types should be accepted:
            // TODO: http://www.youtube.com/watch?v=yRhesBaRCME
            // TODO: http://youtu.be/yRhesBaRCME
            // TODO: http://youtu.be/yRhesBaRCME?t=1m1s
            // TODO: //www.youtube.com/embed/yRhesBaRCME
            // TODO: http://vimeo.com/61361236
            // TODO: //player.vimeo.com/video/61361236

            this.attachments.push(model);
        }
        shooting() {
            this.isshooting = true;
            $('.role-shooting-autocomplete').parent().show();

        }
        posting() {
            this.isshooting = false;

            $('.role-shooting-autocomplete').parent().hide();
        }
        showSave() {
            this.$el.find(".role-show-when-post-edit").slideDown();
        }
        addGenre(event) {
            if (event.keyCode == 13) {
                var genre = this.$(".role-shooting-autocomplete").val();
                this.$(".role-shooting-autocomplete").val('');
                if (this.genres.length < 3) {
                    this.genres.push(genre);
                    this.$(".role-shooting-genres").append("<div>#" + genre + "</div>");
                }
            }
        }
        toggleVideo(event) {
            this.$(".role-video-input-box").toggle();
        }
    }
}