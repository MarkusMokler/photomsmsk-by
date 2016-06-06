module App.Views {
    export class CalendarChooserModal extends Backbone.View<Backbone.Model>{
        constructor(model?) {
            this.template = App.Tools.getTemplate("#calendar-chooser-view-template");
            this.primary = App.Tools.getTemplate("#calendar-primary-row-view-template");
            this.secondary = App.Tools.getTemplate("#calendar-secondary-row-view-template");
            this.events = {
                "click .uk-modal": "close",
                "click .uk-modal-close": "close",
                "click .uk-modal-dialog": "none",
                "click li > div": "hallSelect",
                "click .role-save-halls": "saveHalls"
            }
            super(model);
        }
        
        template: (...data: any[]) => string;
        primary: (...data: any[]) => string;
        secondary: (...data: any[]) => string;

        initialize(options) {

        }
        render() {
            this.$el.html(this.template());
            var self = this;

            $.each(this.model.get('primary'), function () {
                self.$el.find("#primary-select").append(self.primary(this));
            });
            var sec = this.model.get('secondary');
            if (Array.isArray(sec)) {
                $.each(sec, function () {
                    self.$el.find("#secondary-select").append(self.secondary(this));
                });
            }
            return this;
        }

        close() {
            this.remove();
            this.trigger("close");
        }
        none() {
            return false;
        }
        hallSelect(event) {
            $(event.currentTarget).toggleClass("uk-active");
        }
        saveHalls() {
            var arr = [];
            this.$("li > div.uk-active").each(function () {
                arr.push($(this).attr("data-calendarID"));
            });
            this.remove();
            this.trigger("save", arr);
        }
    }
}