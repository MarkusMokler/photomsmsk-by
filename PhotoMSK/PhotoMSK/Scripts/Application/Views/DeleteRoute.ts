module App.Views {
    export class PhoneCheckView extends Backbone.View<PhoneCheckModel>{
        constructor(model?) {
            this.el = "body";
            this.events = {
                "click [data-role-send-phone-to-validate]": "sendToValidate",
                "click [data-role-close-modal-phone-request]": "closeModal",
                "click [data-role-send-phone-confirm]": "confirm"
            };
            super(model);
        }

        shortcut: string;
        id: string;
        key: number;

        initialize(options) {
            this.id = options.id;
            this.shortcut = options.shortcut;
            this.model = new PhoneCheckModel();
            this.listenTo(this.model, "sync", this.onSuccess);
            this.model.url = this.model.urlRoot + this.shortcut + "/phonecheck";
        }

        render() {
            return this;
        }

        sendToValidate() {
            this.model.save({
                EntityID: this.id,
                Number: this.$el.find("[data-role-phone-number]").val()
            });
        }

        onSuccess(model, resp) {
            if (resp.action === 0) {
                $("#modal-phone-request").show();
            }
            if (resp.action === 1) {
                window.location.href = App.makeUrl("/Admin/DeleteRoute/Confirm?shortcut=" + this.shortcut);
            }
            UIkit.notify(resp.message);
        }

        closeModal() {
            $("[data-role-confirm-code]").val("");
            $("#modal-phone-request").hide();
        }

        confirm() {
            this.model.set("ConfirmCode", this.$el.find("[data-role-confirm-code]").val());
            this.model.set("id", "1");
            this.model.save();
        }
    }

    export class ConfirmView extends Backbone.View<ConfirmModel>{
        constructor(model?) {
            this.el = "body";
            this.events = {
                "click [data-role-select-reason]": "selectReason",
                "click .radio": "showTextArea"
            };
            super(model);
        }

        shortcut: string;
        id: string;
        reason: string;

        initialize(options) {
            this.id = options.id;
            this.shortcut = options.shortcut;
            this.model = new ConfirmModel();
            this.listenTo(this.model, "sync", this.onSuccess);
            this.model.url = this.model.urlRoot + this.shortcut + "/deleterequest";
        }

        render() {
            return this;
        }

        selectReason() {
            if ($("#r1").prop("checked")) {
                this.reason = "I no longer want to receive these emails";
            }
            if ($("#r2").prop("checked")) {
                this.reason = "I never signed up for this mailing list";
            }
            if ($("#r3").prop("checked")) {
                this.reason = "The emails are inappropriate";
            }
            if ($("#r4").prop("checked")) {
                this.reason = "The emails are spam and should be reported";
            }
            if ($("#r5").prop("checked") && $("#unsub-reason-desc").val() !== "") {
                this.reason = $("#unsub-reason-desc").val();
            }
            if (this.reason != undefined) {
                this.model.save({
                    reason: this.reason
                });
            } else {
                UIkit.notify("Выберите причину");
            }
        }

        showTextArea() {
            if ($("#r5").prop("checked")) {
                $("#unsub-reason-desc").show();
            } else {
                $("#unsub-reason-desc").hide();
            }
        }

        onSuccess(model, resp) {
            window.location.href = App.makeUrl("/Admin/Home");
            UIkit.notify(resp.message);
        }
    }
} 