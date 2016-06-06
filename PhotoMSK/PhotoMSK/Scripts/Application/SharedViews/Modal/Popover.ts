module App.Views {

    export class AdminEventPopover extends Backbone.View<Backbone.Model> {
        constructor(model?) {
            this.template = App.Tools.getTemplate("#popover-view-template");
            this.className = "uk-button-dropdown uk-open";
            this.events = {
                "click .uk-modal": "close",
                "click .uk-modal-close": "close",
                "click .uk-modal-dialog": "none",
                "click .role-penalty-add": "penaltySave",
                "click .role-close-popover": "close",
                "click .role-penalty-show": "showPenalty",
                "click .role-button-delete": "deleteEvent"
            }
            super(model);
        }
        
        template: (...data: any[]) => string;
        
        initialize(options) { }
        render() {
            this.$el.html(this.template(this.model));
            this.$el.css("position", "absolute");
            this.$el.css("z-index", "100");
            return this;
        }

        close() {
            this.trigger("close");
            this.remove();
        }
        none() {
            return false;
        }
        showPenalty() {
            this.$el.find(".role-penalty-form").toggle();
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
        deleteEvent() {
            var self = this;
            var mm = new App.Models.EventModel(this.model);
            mm.destroy({
                success: function (model, response) {
                    self.close();
                }
            });
        }
    }
}