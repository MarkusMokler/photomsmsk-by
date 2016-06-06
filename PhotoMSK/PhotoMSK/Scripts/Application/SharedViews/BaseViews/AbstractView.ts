/// <reference path="../../../typings/jquery/jquery.d.ts"/>
/// <reference path="../../../typings/jqueryui/jqueryui.d.ts"/>
/// <reference path="../../../typings/underscore/underscore.d.ts"/>
/// <reference path="../../../typings/backbone/backbone.d.ts"/>
/// <reference path="../../../typings/marionette/marionette.d.ts"/> 
/// <reference path="../../../typings/uikit/uikit.d.ts"/> 
/// <reference path="../../Tools.ts"/> 

module App.BaseViews {

    export class BaseView<T extends Backbone.Model> extends Marionette.ItemView<T> {

        constructor(options: Backbone.ViewOptions<T>) {
            super(options);
        }

        changeInput(evt) {
        var changed = evt.currentTarget;
            var value = $(evt.currentTarget).val();
            if (document.location.hostname == "localhost")
                UIkit.notify("Изменено! " + changed.id + " " + value, { status: 'danger', pos: 'bottom-right' });
            this.model.set(this.toCamelCase(changed.id), value);
        }

        updateCover(url: string) {
            this.model.save({ 'coverImage': url });
        }
        updateAvatar(url: string) {
            this.model.save({ 'teaserImage': url });
        }

        save() {
            this.model.save();
        }

        //success(model, resp, options) {
        //    $(".error").removeClass("error");
        //    $(".field-validation-valid").hide();
        //    UIkit.notify("Изменения успешно сохранены", { status: 'success', pos: 'bottom-right' });
        //}
        savePhone() {
            var self = this;
            $("#modal-phone-required").hide();

            $.post("/vjson/Phones/SubmitPhone",
                {
                    ID: self.model.get("id"),
                    ConfirmCode: self.$el.find("[data-role-sms-verification]").val()
                }, data => {
                    if (typeof data.error == "undefined") {
                        UIkit.notify(data.message);
                    } else {
                        UIkit.notify(data.message);
                    }

                });

        }
        sendToValidate() {
            var self = this;
            $("#modal-phone-required").show();
            $.post("/vjson/Phones/ValidatePhone", { ID: self.model.get("id"), Number: self.$el.find("[data-role-phone-number]").val() }, data => {
                UIkit.notify(data.message);
            });

        }

        //next() {
        //    this.model.save();
        //}

        //prev() {
        //    this.$el.find("[data-role-menu]").find("li.uk-active").removeClass("uk-active").prev("li").addClass("uk-active");
        //    this.$el.find("[data-role-content]").find("li.uk-active").removeClass("uk-active").prev("li").addClass("uk-active");
        //}
        //success(model, resp, options) {
        //    this.model.set("id", resp.id);

        //    this.$el.find("[data-role-menu]").find("li.uk-active").removeClass("uk-active").addClass("uk-success").next("li").removeClass('uk-disabled').addClass("uk-active");
        //    this.$el.find("[data-role-content]").find("li.uk-active").removeClass("uk-active").next("li").addClass("uk-active");

        //}

        error(model, xhr, options) {
            var response = xhr.responseJSON;
            $(".error").removeClass('error');
            $(".field-validation-valid").hide();

            //$.UIkit.notify(response.message);
            UIkit.notify("Ошибка! " + response.message, { status: 'danger', pos: 'bottom-right' });
            $.each(response.modelState, (key, value) => {
                $("#" + key.replace('model.', '')).addClass("error");
                $.each(value, function () {
                    $("#" + key.replace("model.", '')).parent().find('span').html(this).show();

                });
            });
        }

        toCamelCase(str: string): string {
            return str.toLowerCase().replace(/-(.)/g, (match, group1) => group1.toUpperCase());

        }

        closeModal() {
            $("#modal-phone-required").hide();
        }

        redirectToAdminPanel() {
            window.location.href = App.makeUrl("/Admin/Home");
        }

    }

    export class BaseEditView<T extends Backbone.Model> extends BaseView<T> {
        constructor(options: Backbone.ViewOptions<T>) {
            super(options);
        }

        success(model, resp, options) {
            this.model.set("id", resp.id);
            UIkit.notify(resp.message);
        }

        deletePhone(e) {
            var self = this;
            var temp = $(e.currentTarget).parent().find("input").val();

            $.ajax({
                url: App.makeUrl("/vjson/Phones/DeletePhone"),
                method: "DELETE",
                data: { ID: self.model.get("id"), Number: temp }
            }).done(data => {
                if (typeof data.error == "undefined") {
                    UIkit.notify(data.message);
                    $(e.currentTarget).parent().parent().empty();
                } else {
                    UIkit.notify(data.message);
                }
            });
        }

    }

    export class BaseCreateView<T extends Backbone.Model> extends BaseView<T> {
        myMap: any;
        placemark: any;
        success(model, resp, options) {
            this.model.set("id", resp.id);

            this.$el.find("[data-role-menu]").find("li.uk-active").removeClass("uk-active").addClass("uk-success").next("li").removeClass('uk-disabled').addClass("uk-active");
            this.$el.find("[data-role-content]").find("li.uk-active").removeClass("uk-active").next("li").addClass("uk-active");
        }

        prev() {
            this.$el.find("[data-role-menu]").find("li.uk-active").removeClass("uk-active").prev("li").addClass("uk-active");
            this.$el.find("[data-role-content]").find("li.uk-active").removeClass("uk-active").prev("li").addClass("uk-active");
        }


    }

}