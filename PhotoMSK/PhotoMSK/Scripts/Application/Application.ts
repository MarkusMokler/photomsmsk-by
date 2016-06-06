///<reference path="../typings/jquery.cookie/jquery.cookie.d.ts"/>
///<reference path="../typings/Hubs/Hubs.d.ts"/>

module App {
    export declare var routeId: string;
    export declare var owner: string;
    export declare var currentView: Backbone.View<Backbone.Model>;
    export var WallView: App.Views.WallView;

    export function makeUrl(url: string): string {
        const baseUrl = $("#config-settings").attr("data-application-url");
        let murl = typeof baseUrl != "undefined" ? baseUrl : "/";
        if (url.indexOf("/") === 0)
            url = url.substr(1);
        if (murl.lastIndexOf("/") !== murl.length - 1)
            murl = murl + "/";
        return murl + url;
    }

    $(() => {

        var chat = $.connection.messageHub;
        // Create a function that the hub can call back to display messages.
        chat.client.addNewMessageToPage = (id, message) => {
            $("." + id + "-role-messages").append("<p>" + message + "</p>");
        };
        // Get the user name and store it to prepend to messages.
        // Start the connection.
        $(".chat-caller").click(function () {
            var $tar = $(this);
            var mm = new Views.MessageModal({
                chat: chat,
                userID: $tar.attr("data-id")
            });
            mm.render().$el.appendTo("body");
        });

        var call = $.connection.callerHub;
        call.client.showInfoByNumber = function (number) {
            var mobile = $('.uk-mobile').clone();

            mobile.appendTo('body').show();
            mobile.click(function () {
                window.open('http://photomsk.by/admin/callhandler/index/' + number, '_blank');
                mobile.remove();
            });
        }

        $.connection.hub.start().done(() => {
            $('#sendmessage').click(() => {
                // Call the Send method on the hub. 
                chat.server.send($('#displayname').val(), $('#message').val());
                // Clear text box and reset focus for next comment. 
                $('#message').val('').focus();
            });
        });
    });
}



$.fn.insertAt = function ($children, index) {
    return this.each(function () {
        var $this = $(this);

        if (index === 0) {
            $this.prepend($children);
        } else {

            if (index === undefined) {
                $this.append($children);
            } else {
                $this.children().eq(index - 1).after($children);
            }
        }
    });
};

$.fn.template = function () {
    var html = this.html();
    if (html != undefined)
        return html.split("%3C%25-%20").join("<%- ").split("%20%25%3E").join(" %>").split("&#39;").join("'");
    return "";
};
