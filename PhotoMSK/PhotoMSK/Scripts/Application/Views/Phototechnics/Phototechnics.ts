module App.Views {
    import PhototechnicModel = Models.PhototechnicModel;

    export class PhototechnicSummary extends Backbone.View<PhototechnicModel>{
        constructor(model?) {
            this.vertical = Tools.getTemplate("#phototechnic-vertical-summary-template");
            this.horizontal = Tools.getTemplate("#phototechnic-horizontal-summary-template");
            this.search = Tools.getTemplate("#phototechnic-search-summary-template");
            this.tagName = "li";
            this.events = {
                "click .role-show-calendar": "click",
                "click [data-role-add-to-cart]": "addToCart"
            };
            super(model);
        }

        vertical: (...data: any[]) => string;
        horizontal: (...data: any[]) => string;
        search: (...data: any[]) => string;
        version: any;

        initialize(options) {
            this.version = options.version;
            if (typeof options.className != 'undefined')
                this.className += options.className;
        }

        render() {
            if (typeof (this.version) == 'undefined')
                this.$el.html(this.vertical(this.model.toJSON()));
            else {

            }
            switch (this.version) {
                case "vertical":
                    this.$el.html(this.vertical(this.model.toJSON()));
                    break;
                case "horizontal":
                    this.$el.html(this.horizontal(this.model.toJSON()));
                    break;
                case "search":
                    this.$el.html(this.search(this.model.toJSON()));
                    break;
                default:
                    this.$el.html(this.vertical(this.model.toJSON()));
            }

            return this;
        }
        addToCart(event) {
            this.trigger("addToCart", this.model.get("id"));
        }
        click(event) {
            new App.Views.CalendarModal({ model: this.model }).render().$el.appendTo("body");
        }
    }

    export class Phototrchnics extends Backbone.View<PhototechnicModel>{
        constructor(model?) {
            this.events = {
            
            };
            super(model);
        }
        phototechnics: Collections.PhototechnicsCollection;
        

        initialize(options) {

            this.phototechnics = new Collections.PhototechnicsCollection();

            this.listenTo(this.phototechnics, 'reset', this.addTechnics);
            this.listenTo(this.phototechnics, 'add', this.addTechnic);

        }
        render() {
            return this;
        }

        addTechnics() {
            this.phototechnics.each(this.addTechnic);
            return this;
        }
        addTechnic(model) {
            var view = new PhototechnicView({ model: model });
            $(".role-PhototechnicsViewModel-placeholder").append(view.render().el);
        }
    }
    export class PhototechnicView extends Backbone.View<PhototechnicModel> {
        //    template:_.template($("#PhototechnicView").html()),
     
        initialize(options) { }
        render() {
            return this;
        }


        click(event) { }
    }
} 