///<reference path="../../Tools.ts"/>
module App.Views {
    import Template = Tools.getTemplate;

    export class AdminEventModal extends Backbone.View<Backbone.Model> {
        constructor(model?) {
            this.template = Template("#admin-event-modal-view-template");
            this.events = {
                "click .uk-modal": "close",
                "click .uk-modal-close": "close",
                "click .uk-modal-dialog": "none",
                "click .role-penalty-save-button": "penaltySave"
            }
            super(model);
        }
        
        template: (...data:any[]) => string;

        initialize(options) { }
        render() {
            this.$el.html(this.template(this.model));
            return this;
        }

        close() {
            this.remove();
        }
        none() {
            return false;
        }
        penaltySave() {

            var self = this;

            var model = new App.Models.PenaltyModel();
            var penalty = {
                eventId: this.model.id,
                description: this.$el.find(".role-description").val(),
                price: this.$el.find(".role-price").val()
            }

            model.save(penalty, {
                success: function (item, response) {
                    self.remove();
                }
            });
            return false;
        }
    }
}
