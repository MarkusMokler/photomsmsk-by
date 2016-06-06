module App.Views {
    export class PageCategoryModel extends Backbone.Model {
        urlRoot = App.makeUrl("/api/TextPageCategory")
    }
  
    export class MenuItemModel extends Backbone.Model {

    }

    export class ItemView extends Backbone.View<Backbone.Model>{
        template: (any) => string;
        initialize() {
            this.template = _.template("<option id='<%- id %>' value='<%- id %>'><%- categoryName %></option>");

        }
        render() {
            this.setElement(this.template(this.model.toJSON()));
            return this;
        }
    }

    export class PageCategories extends Backbone.Collection<PageCategoryModel>{
        constructor(model?) {
            this.url = App.makeUrl("/api/TextPageCategory");
            super(model);
        }

        url: string;
    }
    
    export class CollectionView extends Backbone.View<Backbone.Model>{
        constructor(model?) {
            this.tagName = "select";           
            this.events = {
                "change": "selectItem"
            }
            super(model);
        }
        
        collection: PageCategories;

        initialize(options) {
            var routeId = options.routeId;
            this.collection = new PageCategories();
            this.collection.on('sync', this.render, this);
            this.collection.fetch({ data: { routeID: routeId } });
        }
        selectItem(event) {
            this.trigger("CategoryChanged", $(event.currentTarget).val());
        }
        render() {
            _.each(this.collection.models, function (item) {
                this.$el.append(new ItemView({ model: item }).render().el);
            }, this);
            return this;
        }
    };

    export class MenuEdit extends App.BaseViews.BaseEditView<MenuItemModel> {
        constructor(model?) {
            this.el = "body";
            this.events = {
                "click [data-role-save-button]": "save",
                "change [data-role-input]": "changeInput",
                "change [data-role-changePageType]": "changePageType"
            };
            super(model);
        }

        initialize(options) {
            var self = this;
            self.model = new MenuItemModel({ routeId: App.owner });
            this.listenTo(this.model, "sync", this.success);
            this.listenTo(this.model, "error", this.error);
        }

        render() {
            var self = this;
            var ac = $("input.uk-search-field").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: App.makeUrl("/Api/TextPage"),
                        dataType: "json",
                        data: {
                            routeID: App.owner,
                            name: request.term
                        },
                        success(data) {
                            response($.map(data, function (item) {
                                return {
                                    label: item.title,
                                    value: item.id
                                }
                            }));
                        }
                    });
                },
                minLength: 3,
                select(event, ui) {
                    self.model.set("textPageID", ui.item.value);
                },
                open() {
                    $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                },
                close() {
                    $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                }
            });

            return this;
        }

        //ac._renderItem = function( ul, item ) {
        //    return $("<li>").html(item.label).appendTo( ul );
        // };
        //},
        changePageType(evt) {
            var elem = $(evt.currentTarget).val();
            this.model.set("type", elem);
            switch (elem) {
                case "PageMenuItem":
                    $("#LinkMenuItem").hide();
                    $("#PageMenuItem").show();

                    break;
                case "LinkMenuItem":
                    $("#PageMenuItem").hide();
                    $("#LinkMenuItem").show();
                    break;
                default:
            }

        }
        save() {
            this.model.save();
        }
    }
}

