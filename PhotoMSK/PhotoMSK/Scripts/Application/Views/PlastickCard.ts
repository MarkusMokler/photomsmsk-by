///<reference path="../SharedViews/BaseViews/AbstractView.ts"/>

module App.PlastickCard {
    export class PlastickCardModel extends Backbone.Model {
        constructor(model?) {
            this.urlRoot = App.makeUrl("/api/v2/PlastickCard");
            super(model);
        }
    }

    export class CreateView extends BaseViews.BaseCreateView<PlastickCardModel> {
        constructor(model?) {
            this.events = {
                "change input": "changeInput",
                //"change [data-role-input-optional]": "changeInput",
                //"change [data-role-input-required]": "checkRequired",
                "click [data-role-close-modal]": "closeModal",
                "click [data-role-next-step]": "save",
                "click [data-role-prev-step]": "prev",
                "click [data-role-save-phone]": "savePhone",
                "click [data-role-send-to-validate]": "sendToValidate",
                "click [data-role-to-adminpanel]": "redirectToAdminPanel",
                "click [data-role-to-edit]": "redirectToEdit",
                "click [data-role-to-page]": "redirectToMyPage"
            };
            super(model);
        }


    }
}