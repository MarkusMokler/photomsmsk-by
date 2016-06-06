module App.Views {

    export class VideoModel extends Backbone.Model {

    }

    export class VideoUploadView extends Backbone.View<VideoModel>{
        constructor(model?) {
            this.el = "body";
            this.template = App.Tools.getTemplate("#video-upload-view-template");
            this.className = "attachment";
            this.events = {
                "change .VideoInput": "fileChanged",
                "change .Description": "descriptionChanged",
                "click .delete": "deleteModel",
                "click .wall-post-save-video": "saveVideo",
            }
            super(model);
        }

        modal: JQuery;
        template: (...data:any[]) => string;
        
        initialize(parent) {
            this.model.set('type', "video");
        }
        render() {
            var template = $(this.template());
            this.$el.append(template);
            this.modal = new UIkit.modal('#' + template.attr("id"));
            this.modal.show();
            return this;
        }

        deleteModel() {
            this.$el.find(".attachment").hide();
            this.model.destroy();
        }

        fileChanged(event) {
            this.preview(this.$el.find(".VideoInput").val());
        }
        descriptionChanged(event) {
            this.model.set("title", this.$el.find(".Description").val());
        }
        preview(videoUri) {
            if ((!videoUri.match('http://*')) && (!videoUri.match('https://*'))) {
                return;
            }

            if (videoUri.indexOf("youtu") != -1) {

                var array = videoUri.split("?");

                var array2 = array[1].split("=");


                this.$el.find(".yuotube-video").attr("src", "https://www.youtube.com/embed/" + array2[1]);

                this.model.set("url", "https://www.youtube.com/embed/" + array2[1]);
                this.model.set("reference", "https://www.youtube.com/embed/" + array2[1]);

                this.$el.find(".youtube").show();
                this.trigger("added");
            }

            if (videoUri.indexOf("vimeo") != -1) {
                var array = videoUri.split("com/");
                this.$el.find(".vimeo-video").attr("src", " http://player.vimeo.com/video/" + array[1]);
                this.model.set("url", "http://player.vimeo.com/video/" + array[1]);
                this.model.set("reference", "http://player.vimeo.com/video/" + array[1]);
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
        }

        saveVideo() {
            this.modal.hide();
        }
    }
}