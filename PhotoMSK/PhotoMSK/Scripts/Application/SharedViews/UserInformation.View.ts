///<reference path="../SharedViews/BaseViews/AbstractView.ts"/>
module App.Views {
    import UserInformationModel = App.Models.UserInformationModel;

    export class UserInformation extends Backbone.View<UserInformationModel>{
        constructor(model?) {
            this.template = App.Tools.getTemplate("#user-information-view-template");
            this.events = {
                "click .clicl": "click"
            }
            super(model);
        }
        
        template: (...data: any[]) => string;
        
        initialize(options) { }
        render() {
            this.setElement(this.template(this.model));
            if (this.model.penalties != null && typeof this.model.penalties != 'undefined' && this.model.penalties.length > 0)
                this.$el.find("#penalty-list-placeholder").html(new Views.PenaltyList({ model: this.model}).render().el);
            return this;
        }

        click(event) { }
    }
}