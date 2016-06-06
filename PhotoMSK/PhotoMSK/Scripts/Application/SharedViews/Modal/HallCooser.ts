module App.Views {
    import HallModel = App.Halls.HallModel;

    export class HallCooserModal extends Backbone.View<HallModel>{
        constructor(model?) {
            this.template = App.Tools.getTemplate("#hall-chooser-view-template");
            this.hall = App.Tools.getTemplate("#hall-summary-template");
            this.events = {
                "click .uk-modal": "close",
                "click .uk-modal-close": "close",
                "click .uk-modal-dialog": "none",
                "click #hall-select li": "hallSelect",
                "click .role-save-halls": "saveHalls"
            }
            super(model);
        }
        
        hall: (...data: any[]) => string;
        template: (...data: any[]) => string;

        initialize (options) {

        }
        render () {
            this.$el.html(this.template());
            var self = this;

            $.each(this.model, function () {
                self.$el.find("#hall-select").append(self.hall(this));
            });
            return this;
        }

        close () {
            this.remove();
            this.trigger("close");
        }
        none () {
            return false;
        }
        hallSelect (event) {
            $(event.currentTarget).toggleClass("uk-active");
        }
        saveHalls () {
            var arr = [];
            this.$("#hall-select li.uk-active").each(function () {
                arr.push($(this).attr("data-value"));
            });
            this.remove();
            this.trigger("save", arr);
        }
    }
}