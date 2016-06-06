module App.Views {

    export class MessageModal extends Backbone.View<Backbone.Model> {
        constructor(model?) {
            this.template = App.Tools.getTemplate("#message-view-template");
            this.src = "";
            this.events = {
                "click .uk-modal": "close",
                "click .uk-modal-close": "close",
                "click .uk-modal-dialog": "none",
                "click .role-close-dialog": "close",
                "click .role-send-message": "sendMessage"
            }
            super(model);
        }
        
        template: (...data: any[]) => string;
        src: string;
        chat: any;
        userID: string;
        

        initialize(options) {
            this.chat = options.chat;
            this.userID = options.userID;

        }
        render() {
            var self = this;
            this.$el.html(this.template({ userId: self.userID }));
            return this;
        }

        close() {
            this.remove();
            this.trigger("close", this.model);
        }
        none() {
            return false;
        }
        sendMessage(parameters) {
            var message = this.$el.find(".role-message-text").val();
            this.chat.server.send(this.userID, message);
        }
    }
}