///<reference path="../BaseViews/AbstractView.ts"/>

module App.Views {
    import UserInformationModel = App.Models.UserInformationModel;

    export class PenaltyList extends Backbone.View<UserInformationModel>{
        constructor(model?) {
            this.template = Tools.getTemplate("#penalties-list-view-template");
            this.events = {
                "click .clicl": "click"
            }
            super(model);
        }
        
        template: (...data: any[]) => string;

        initialize(options) { }
        render() {
            this.setElement(this.template(this.model));
            $.each(this.model.penalties, (index, value) => {
                this.$el.find("#penalties-placeholder").append(new App.Views.PenaltySummary({ model: value }).render().el);
            });
            return this;
        }

        click(event) { }
    }

    export class PenaltySummary extends Backbone.View<Backbone.Model>{
        constructor(model?) {
            this.template = Tools.getTemplate("#penalty-summary-template");
            this.events = {
                "click .clicl": "click"
            }
            super(model);
        }
        
        template: (...data: any[]) => string;
        
        initialize(options) { }
        render() {
            this.setElement(this.template(this.model));
            return this;
        }

        click(event) { }
    }
}