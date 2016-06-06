import Tools = App.Tools;

module App.Views {
    export class ToturialModal extends Backbone.View<Backbone.Model> {
        constructor(model?) {
            this.template = Tools.getTemplate("#toturial-view-template");
            this.src = "";
            this.events = {
                "click .uk-modal": "close",
                "click .uk-modal-close": "close",
                "click .uk-modal-dialog": "none",
                "click .role-close-dialog": "close"
            }
            super(model);
        }
        
        template: (...data: any[]) => string;
        src: string;

        initialize(options) {
            this.src = options.src;
        }
        render() {
            this.$el.html(this.template({ src: this.src }));
            return this;
        }

        close() {
            this.remove();
            this.trigger("close", this.model);
        }
        none() {
            return false;
        }
    }
}