///<reference path="../typings/jquery.cookie/jquery.cookie.d.ts"/>
///<reference path="../typings/Hubs/Hubs.d.ts"/>
var App;
(function (App) {
    App.WallView;
    function makeUrl(url) {
        var baseUrl = $("#config-settings").attr("data-application-url");
        var murl = typeof baseUrl != "undefined" ? baseUrl : "/";
        if (url.indexOf("/") === 0)
            url = url.substr(1);
        if (murl.lastIndexOf("/") !== murl.length - 1)
            murl = murl + "/";
        return murl + url;
    }
    App.makeUrl = makeUrl;
    $(function () {
        var chat = $.connection.messageHub;
        // Create a function that the hub can call back to display messages.
        chat.client.addNewMessageToPage = function (id, message) {
            $("." + id + "-role-messages").append("<p>" + message + "</p>");
        };
        // Get the user name and store it to prepend to messages.
        // Start the connection.
        $(".chat-caller").click(function () {
            var $tar = $(this);
            var mm = new App.Views.MessageModal({
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
        };
        $.connection.hub.start().done(function () {
            $('#sendmessage').click(function () {
                // Call the Send method on the hub. 
                chat.server.send($('#displayname').val(), $('#message').val());
                // Clear text box and reset focus for next comment. 
                $('#message').val('').focus();
            });
        });
    });
})(App || (App = {}));
$.fn.insertAt = function ($children, index) {
    return this.each(function () {
        var $this = $(this);
        if (index === 0) {
            $this.prepend($children);
        }
        else {
            if (index === undefined) {
                $this.append($children);
            }
            else {
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
//App.Tools.htmlSubstring = function (s, n) {
//    var m, r = /<([^>\s]*)[^>]*>/g,
//        stack = [],
//        lasti = 0,
//        result = '';
//    while ((m = r.exec(s)) && n) {
//        var temp = s.substring(lasti, m.index).substr(0, n);
//        result += temp;
//        n -= temp.length;
//        lasti = r.lastIndex;
//        if (n) {
//            result += m[0];
//            if (m[1].indexOf('/') === 0) {
//                stack.pop();
//            } else if (m[1].lastIndexOf('/') !== m[1].length - 1) {
//                stack.push(m[1]);
//            }
//        }
//    }
//    result += s.substr(lasti, n);
//    while (stack.length) {
//        result += '</' + stack.pop() + '>';
//    }
//    return result;
//}
//Number.prototype.formatMoney = function (decPlaces, thouSeparator, decSeparator) {
//    var n = this,
//        decPlaces = isNaN(decPlaces = Math.abs(decPlaces)) ? 0 : decPlaces,
//        decSeparator = decSeparator == undefined ? "." : decSeparator,
//        thouSeparator = thouSeparator == undefined ? " " : thouSeparator,
//        sign = n < 0 ? "-" : "",
//        i = parseInt(n = Math.abs(+n || 0).toFixed(decPlaces)) + "",
//        j = (j = i.length) > 3 ? j % 3 : 0;
//    return sign + (j ? i.substr(0, j) + thouSeparator : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thouSeparator) + (decPlaces ? decSeparator + Math.abs(n - i).toFixed(decPlaces).slice(2) : "");
//};
//function guid() {
//    function s4() {
//        return Math.floor((1 + Math.random()) * 0x10000)
//            .toString(16)
//            .substring(1);
//    }
//    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
//        s4() + '-' + s4() + s4() + s4();
//} 
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var App;
(function (App) {
    var Collections;
    (function (Collections) {
        var PhotographersCollection = (function (_super) {
            __extends(PhotographersCollection, _super);
            function PhotographersCollection(model) {
                this.url = App.makeUrl("/Api/Photographer");
                _super.call(this, model);
            }
            PhotographersCollection.prototype.initialize = function (options) {
            };
            return PhotographersCollection;
        })(Backbone.Collection);
        Collections.PhotographersCollection = PhotographersCollection;
    })(Collections = App.Collections || (App.Collections = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Collections;
    (function (Collections) {
        var PhotorentsCollection = (function (_super) {
            __extends(PhotorentsCollection, _super);
            function PhotorentsCollection(model) {
                this.url = App.makeUrl("/Api/Photorents");
                _super.call(this, model);
            }
            PhotorentsCollection.prototype.initialize = function (options) {
            };
            return PhotorentsCollection;
        })(Backbone.Collection);
        Collections.PhotorentsCollection = PhotorentsCollection;
    })(Collections = App.Collections || (App.Collections = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Collections;
    (function (Collections) {
        var PhototechnicsCollection = (function (_super) {
            __extends(PhototechnicsCollection, _super);
            function PhototechnicsCollection(model) {
                this.url = App.makeUrl("/Api/PhototechnicsViewModel");
                _super.call(this, model);
            }
            PhototechnicsCollection.prototype.initialize = function (options) {
            };
            return PhototechnicsCollection;
        })(Backbone.Collection);
        Collections.PhototechnicsCollection = PhototechnicsCollection;
    })(Collections = App.Collections || (App.Collections = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Models;
    (function (Models) {
        var WallModel = (function (_super) {
            __extends(WallModel, _super);
            function WallModel(model) {
                this.url = App.makeUrl("/Api/Wall");
                _super.call(this, model);
            }
            WallModel.prototype.defaults = function () {
                return {
                    title: ""
                };
            };
            WallModel.prototype.parse = function (data) {
                var response = {};
                return response;
            };
            return WallModel;
        })(Backbone.Model);
        Models.WallModel = WallModel;
        ;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
/// <reference path="../../typings/backbone/backbone.d.ts"/>
/// <reference path="../Models/WallModel.ts"/>
var App;
(function (App) {
    var Collections;
    (function (Collections) {
        var WallCollection = (function (_super) {
            __extends(WallCollection, _super);
            function WallCollection(model) {
                this.url = App.makeUrl("/Api/Wall");
                _super.call(this, model);
            }
            WallCollection.prototype.initialize = function (options) {
            };
            return WallCollection;
        })(Backbone.Collection);
        Collections.WallCollection = WallCollection;
    })(Collections = App.Collections || (App.Collections = {}));
})(App || (App = {}));
/// <reference path="../../typings/backbone/backbone.d.ts"/>
var App;
(function (App) {
    var Models;
    (function (Models) {
        var AttachmentModel = (function (_super) {
            __extends(AttachmentModel, _super);
            function AttachmentModel(model) {
                this.url = App.makeUrl("/Api/Attachments");
                _super.call(this, model);
            }
            AttachmentModel.prototype.parse = function (data) {
                var response = {};
                return response;
            };
            return AttachmentModel;
        })(Backbone.Model);
        Models.AttachmentModel = AttachmentModel;
        ;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
/// <reference path="../../typings/backbone/backbone.d.ts"/>
var App;
(function (App) {
    var Models;
    (function (Models) {
        var BookingModel = (function (_super) {
            __extends(BookingModel, _super);
            function BookingModel(model) {
                this.urlRoot = App.makeUrl("/api/Booking");
                _super.call(this, model);
            }
            return BookingModel;
        })(Backbone.Model);
        Models.BookingModel = BookingModel;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
/// <reference path="../../typings/backbone/backbone.d.ts"/>
var App;
(function (App) {
    var Models;
    (function (Models) {
        var CalendarModel = (function (_super) {
            __extends(CalendarModel, _super);
            function CalendarModel(model) {
                this.urlRoot = App.makeUrl("/api/Calendar");
                _super.call(this, model);
            }
            return CalendarModel;
        })(Backbone.Model);
        Models.CalendarModel = CalendarModel;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
/// <reference path="../../typings/backbone/backbone.d.ts"/>
var App;
(function (App) {
    var Models;
    (function (Models) {
        var HallModel = (function (_super) {
            __extends(HallModel, _super);
            function HallModel(model) {
                this.urlRoot = App.makeUrl("/api/hall");
                _super.call(this, model);
            }
            return HallModel;
        })(Backbone.Model);
        Models.HallModel = HallModel;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
/// <reference path="../../typings/backbone/backbone.d.ts"/>
var App;
(function (App) {
    var Models;
    (function (Models) {
        var MenuItemModel = (function (_super) {
            __extends(MenuItemModel, _super);
            function MenuItemModel(model) {
                this.urlRoot = App.makeUrl("/Api/MenuItem");
                _super.call(this, model);
            }
            return MenuItemModel;
        })(Backbone.Model);
        Models.MenuItemModel = MenuItemModel;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
/// <reference path="../../typings/backbone/backbone.d.ts"/>
var App;
(function (App) {
    var Models;
    (function (Models) {
        var PenaltyModel = (function (_super) {
            __extends(PenaltyModel, _super);
            function PenaltyModel(model) {
                this.url = App.makeUrl("/Api/Penalties");
                _super.call(this, model);
            }
            PenaltyModel.prototype.defaults = function () {
                return {
                    description: ""
                };
            };
            PenaltyModel.prototype.parse = function (data) {
                var response = {};
                return response;
            };
            return PenaltyModel;
        })(Backbone.Model);
        Models.PenaltyModel = PenaltyModel;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Models;
    (function (Models) {
        var PhototechnicModel = (function (_super) {
            __extends(PhototechnicModel, _super);
            function PhototechnicModel() {
                _super.apply(this, arguments);
            }
            return PhototechnicModel;
        })(Backbone.Model);
        Models.PhototechnicModel = PhototechnicModel;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
/// <reference path="../../../typings/backbone/backbone.d.ts"/>
var App;
(function (App) {
    var Models;
    (function (Models) {
        var PhototechnicsModel = (function (_super) {
            __extends(PhototechnicsModel, _super);
            function PhototechnicsModel(model) {
                this.url = App.makeUrl("/Api/PhototechnicsViewModel");
                _super.call(this, model);
            }
            PhototechnicsModel.prototype.defaults = function () {
                return {
                    title: ""
                };
            };
            return PhototechnicsModel;
        })(Backbone.Model);
        Models.PhototechnicsModel = PhototechnicsModel;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
/// <reference path="../../../typings/backbone/backbone.d.ts"/>
var App;
(function (App) {
    var Models;
    (function (Models) {
        var PlaceModel = (function (_super) {
            __extends(PlaceModel, _super);
            function PlaceModel(model) {
                this.urlRoot = App.makeUrl("/api/place");
                _super.call(this, model);
            }
            return PlaceModel;
        })(Backbone.Model);
        Models.PlaceModel = PlaceModel;
        ;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
/// <reference path="../../typings/backbone/backbone.d.ts"/>
var App;
(function (App) {
    var Models;
    (function (Models) {
        var TextPageModel = (function (_super) {
            __extends(TextPageModel, _super);
            function TextPageModel(model) {
                this.urlRoot = App.makeUrl("/Api/TextPage");
                _super.call(this, model);
            }
            return TextPageModel;
        })(Backbone.Model);
        Models.TextPageModel = TextPageModel;
        ;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Models;
    (function (Models) {
        var UserInformationModel = (function (_super) {
            __extends(UserInformationModel, _super);
            function UserInformationModel() {
                _super.apply(this, arguments);
            }
            return UserInformationModel;
        })(Backbone.Model);
        Models.UserInformationModel = UserInformationModel;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Routers;
    (function (Routers) {
        var PhotomskRouter = (function (_super) {
            __extends(PhotomskRouter, _super);
            function PhotomskRouter(options) {
                this.routes = {
                    "": "mainWall",
                    "/": "mainWall",
                    "Home": "mainWall"
                };
                _super.call(this, options);
            }
            PhotomskRouter.prototype.mainWall = function () {
                App.currentView = new App.Views.MainPage();
                App.currentView.render();
            };
            return PhotomskRouter;
        })(Backbone.Router);
        Routers.PhotomskRouter = PhotomskRouter;
    })(Routers = App.Routers || (App.Routers = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Tools;
    (function (Tools) {
        function getTemplate(templateString, settings) {
            if (typeof Tools.templates == "undefined") {
                Tools.templates = {};
            }
            if (typeof Tools.templates[templateString] == "undefined") {
                Tools.templates[templateString] = _.template($(templateString).html(), settings);
            }
            return Tools.templates[templateString];
        }
        Tools.getTemplate = getTemplate;
        ;
        function cookieList(cookieName) {
            //When the cookie is saved the items will be a comma seperated string
            //So we will split the cookie by comma to get the original array
            var cookie = $.cookie(cookieName);
            //Load the items or a new array if null.
            var items = cookie ? $.parseJSON(cookie) : new Array();
            //Return a object that we can use to access the array.
            //while hiding direct access to the declared items array
            //this is called closures see http://www.jibbering.com/faq/faq_notes/closures.html
            return {
                "add": function (val) {
                    //Add to the items.
                    items.push(val);
                    //Save the items to a cookie.
                    //EDIT: Modified from linked answer by Nick see 
                    //      http://stackoverflow.com/questions/3387251/how-to-store-array-in-jquery-cookie
                    $.cookie(cookieName, JSON.stringify(items));
                },
                "remove": function (val) {
                    //EDIT: Thx to Assef and luke for remove.
                    var indx = items.indexOf(val);
                    if (indx != -1)
                        items.splice(indx, 1);
                    $.cookie(cookieName, items.join(','));
                },
                "clear": function () {
                    items = null;
                    //clear the cookie.
                    $.cookie(cookieName, null);
                },
                "items": function () { return items; }
            };
        }
        Tools.cookieList = cookieList;
        function formatMoney(n, decPlaces, thouSeparator, decSeparator) {
            decPlaces = isNaN(decPlaces = Math.abs(decPlaces)) ? 0 : decPlaces;
            decSeparator = decSeparator == undefined ? "." : decSeparator;
            thouSeparator = thouSeparator == undefined ? " " : thouSeparator;
            var sign = n < 0 ? "-" : "";
            var m = "";
            var i = +parseInt(m = Math.abs(+n || 0).toFixed(decPlaces)) + "";
            var j = (j = i.length) > 3 ? j % 3 : 0;
            return sign + (j ? i.substr(0, j) + thouSeparator : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thouSeparator) + (decPlaces ? decSeparator + Math.abs(n - parseInt(i)).toFixed(decPlaces).slice(2) : "");
        }
        Tools.formatMoney = formatMoney;
        ;
        function guid() {
            function s4() {
                return Math.floor((1 + Math.random()) * 0x10000)
                    .toString(16)
                    .substring(1);
            }
            return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
                s4() + '-' + s4() + s4() + s4();
        }
        Tools.guid = guid;
        function htmlSubstring(s, n) {
            var m, r = /<([^>\s]*)[^>]*>/g, stack = [], lasti = 0, result = '';
            while ((m = r.exec(s)) && n) {
                var temp = s.substring(lasti, m.index).substr(0, n);
                result += temp;
                n -= temp.length;
                lasti = r.lastIndex;
                if (n) {
                    result += m[0];
                    if (m[1].indexOf('/') === 0) {
                        stack.pop();
                    }
                    else if (m[1].lastIndexOf('/') !== m[1].length - 1) {
                        stack.push(m[1]);
                    }
                }
            }
            result += s.substr(lasti, n);
            while (stack.length) {
                result += '</' + stack.pop() + '>';
            }
            return result;
        }
        Tools.htmlSubstring = htmlSubstring;
    })(Tools = App.Tools || (App.Tools = {}));
})(App || (App = {}));
///<reference path="../../Tools.ts"/>
var App;
(function (App) {
    var Views;
    (function (Views) {
        var Template = App.Tools.getTemplate;
        var AdminEventModal = (function (_super) {
            __extends(AdminEventModal, _super);
            function AdminEventModal(model) {
                this.template = Template("#admin-event-modal-view-template");
                this.events = {
                    "click .uk-modal": "close",
                    "click .uk-modal-close": "close",
                    "click .uk-modal-dialog": "none",
                    "click .role-penalty-save-button": "penaltySave"
                };
                _super.call(this, model);
            }
            AdminEventModal.prototype.initialize = function (options) { };
            AdminEventModal.prototype.render = function () {
                this.$el.html(this.template(this.model));
                return this;
            };
            AdminEventModal.prototype.close = function () {
                this.remove();
            };
            AdminEventModal.prototype.none = function () {
                return false;
            };
            AdminEventModal.prototype.penaltySave = function () {
                var self = this;
                var model = new App.Models.PenaltyModel();
                var penalty = {
                    eventId: this.model.id,
                    description: this.$el.find(".role-description").val(),
                    price: this.$el.find(".role-price").val()
                };
                model.save(penalty, {
                    success: function (item, response) {
                        self.remove();
                    }
                });
                return false;
            };
            return AdminEventModal;
        })(Backbone.View);
        Views.AdminEventModal = AdminEventModal;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Views;
    (function (Views) {
        var CalendarModal = (function (_super) {
            __extends(CalendarModal, _super);
            function CalendarModal(model) {
                _super.call(this, model);
                this.template = App.Tools.getTemplate("#calendar-modal-view-template");
                this.src = "";
                this.events = {
                    "click .uk-modal": "close",
                    "click .uk-modal-close": "close",
                    "click .uk-modal-dialog": "none",
                    "click .role-close-dialog": "close"
                };
            }
            CalendarModal.prototype.initialize = function (options) {
                this.eventCollection = new App.Collections.EventCollection();
                this.validateEventCollection = new App.Collections.EventCollection();
                //     this.listenTo(this.eventCollection, "add", this.addEvent);
            };
            CalendarModal.prototype.render = function () {
                var self = this;
                this.$el.html(this.template(this.model.toJSON()));
                this.calendar = new App.Views.PhotomskCalendar({ model: this.model.get('rents') });
                this.$('.role-booking-calendar').html(this.calendar.render().el);
                this.calendar.updateView();
                //    this.listenTo(this.calendar, 'eventAdded', this.eventAdded);
                //      var tmp = _.template($("#rentcalendar-header-view-template").template());
                //    this.$(".calendar-header-placeholder").append(tmp({ rents: this.model.get('rents') }));
                return this;
            };
            //addToCart(event) {
            //    var self = this;
            //    $.get("/api/Events", {
            //        validate: true,
            //        start: event.get("start").format(),
            //        end: event.get("end").format(),
            //        calendarIDs: event.get("calendarIDs")
            //    }, function (data) {
            //            if (data.error) {
            //                UIkit.notify({
            //                    message: data.status,
            //                    status: 'danger',
            //                    timeout: 5000,
            //                    pos: 'bottom-right'
            //                });
            //                return;
            //            }
            //            $.each(data, function () {
            //                this.className = 'role-add-booking';
            //                self.calendar.fullCalendar('renderEvent', this, true);
            //                self.$el.find("#booking-placeholder").append(self.bookingTemplate(this));
            //                self.validateEventCollection.add(this);
            //            });
            //        });
            //}
            //booking() {
            //    var self = this;
            //    if (this.validateEventCollection.length == 0) {
            //        UIkit.notify({
            //            message: 'Не выбрано время, бронируемое за человеком =)',
            //            status: 'danger',
            //            timeout: 5000,
            //            pos: 'bottom-right'
            //        });
            //        return;
            //    }
            //    this.validateEventCollection.each(function (model, index) {
            //        model.save(model.toJSON(), {
            //            success: function (model) {
            //                UIkit.notify({
            //                    message: 'Забронированно с ' + new moment(model.get("start")).format("lll") + " по " + new moment(model.get("start")).format("lll"),
            //                    status: 'info',
            //                    timeout: 5000,
            //                    pos: 'bottom-right'
            //                });
            //                $('#calendar').fullCalendar("refetchEvents");
            //                self.validateEventCollection.reset();
            //                self.$el.find("#booking-placeholder").html('');
            //            },
            //            error: function (mdel) {
            //                UIkit.notify({
            //                    message: 'Невозможно забронировать студию с такими параметрами, проверьте введенную информацию',
            //                    status: 'danger',
            //                    timeout: 5000,
            //                    pos: 'bottom-right'
            //                });
            //            }
            //        });
            //    });
            //    this.calendar.fullCalendar('removeEvents', function (event) {
            //        return event.className == "role-add-booking";
            //    });
            //}
            //cleanData() {
            //    this.validateEventCollection.reset();
            //    this.eventCollection.reset();
            //    this.$el.find("#booking-placeholder").html('');
            //    this.calendar.fullCalendar('removeEvents', function (event) {
            //        return event.className == "role-add-booking";
            //    });
            //}
            CalendarModal.prototype.close = function () {
                this.remove();
                this.trigger("close", this.model);
            };
            CalendarModal.prototype.none = function () {
                return false;
            };
            return CalendarModal;
        })(Backbone.View);
        Views.CalendarModal = CalendarModal;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Views;
    (function (Views) {
        var CalendarChooserModal = (function (_super) {
            __extends(CalendarChooserModal, _super);
            function CalendarChooserModal(model) {
                this.template = App.Tools.getTemplate("#calendar-chooser-view-template");
                this.primary = App.Tools.getTemplate("#calendar-primary-row-view-template");
                this.secondary = App.Tools.getTemplate("#calendar-secondary-row-view-template");
                this.events = {
                    "click .uk-modal": "close",
                    "click .uk-modal-close": "close",
                    "click .uk-modal-dialog": "none",
                    "click li > div": "hallSelect",
                    "click .role-save-halls": "saveHalls"
                };
                _super.call(this, model);
            }
            CalendarChooserModal.prototype.initialize = function (options) {
            };
            CalendarChooserModal.prototype.render = function () {
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
            };
            CalendarChooserModal.prototype.close = function () {
                this.remove();
                this.trigger("close");
            };
            CalendarChooserModal.prototype.none = function () {
                return false;
            };
            CalendarChooserModal.prototype.hallSelect = function (event) {
                $(event.currentTarget).toggleClass("uk-active");
            };
            CalendarChooserModal.prototype.saveHalls = function () {
                var arr = [];
                this.$("li > div.uk-active").each(function () {
                    arr.push($(this).attr("data-calendarID"));
                });
                this.remove();
                this.trigger("save", arr);
            };
            return CalendarChooserModal;
        })(Backbone.View);
        Views.CalendarChooserModal = CalendarChooserModal;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Views;
    (function (Views) {
        var HallCooserModal = (function (_super) {
            __extends(HallCooserModal, _super);
            function HallCooserModal(model) {
                this.template = App.Tools.getTemplate("#hall-chooser-view-template");
                this.hall = App.Tools.getTemplate("#hall-summary-template");
                this.events = {
                    "click .uk-modal": "close",
                    "click .uk-modal-close": "close",
                    "click .uk-modal-dialog": "none",
                    "click #hall-select li": "hallSelect",
                    "click .role-save-halls": "saveHalls"
                };
                _super.call(this, model);
            }
            HallCooserModal.prototype.initialize = function (options) {
            };
            HallCooserModal.prototype.render = function () {
                this.$el.html(this.template());
                var self = this;
                $.each(this.model, function () {
                    self.$el.find("#hall-select").append(self.hall(this));
                });
                return this;
            };
            HallCooserModal.prototype.close = function () {
                this.remove();
                this.trigger("close");
            };
            HallCooserModal.prototype.none = function () {
                return false;
            };
            HallCooserModal.prototype.hallSelect = function (event) {
                $(event.currentTarget).toggleClass("uk-active");
            };
            HallCooserModal.prototype.saveHalls = function () {
                var arr = [];
                this.$("#hall-select li.uk-active").each(function () {
                    arr.push($(this).attr("data-value"));
                });
                this.remove();
                this.trigger("save", arr);
            };
            return HallCooserModal;
        })(Backbone.View);
        Views.HallCooserModal = HallCooserModal;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Views;
    (function (Views) {
        var MessageModal = (function (_super) {
            __extends(MessageModal, _super);
            function MessageModal(model) {
                this.template = App.Tools.getTemplate("#message-view-template");
                this.src = "";
                this.events = {
                    "click .uk-modal": "close",
                    "click .uk-modal-close": "close",
                    "click .uk-modal-dialog": "none",
                    "click .role-close-dialog": "close",
                    "click .role-send-message": "sendMessage"
                };
                _super.call(this, model);
            }
            MessageModal.prototype.initialize = function (options) {
                this.chat = options.chat;
                this.userID = options.userID;
            };
            MessageModal.prototype.render = function () {
                var self = this;
                this.$el.html(this.template({ userId: self.userID }));
                return this;
            };
            MessageModal.prototype.close = function () {
                this.remove();
                this.trigger("close", this.model);
            };
            MessageModal.prototype.none = function () {
                return false;
            };
            MessageModal.prototype.sendMessage = function (parameters) {
                var message = this.$el.find(".role-message-text").val();
                this.chat.server.send(this.userID, message);
            };
            return MessageModal;
        })(Backbone.View);
        Views.MessageModal = MessageModal;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Views;
    (function (Views) {
        var AdminEventPopover = (function (_super) {
            __extends(AdminEventPopover, _super);
            function AdminEventPopover(model) {
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
                };
                _super.call(this, model);
            }
            AdminEventPopover.prototype.initialize = function (options) { };
            AdminEventPopover.prototype.render = function () {
                this.$el.html(this.template(this.model));
                this.$el.css("position", "absolute");
                this.$el.css("z-index", "100");
                return this;
            };
            AdminEventPopover.prototype.close = function () {
                this.trigger("close");
                this.remove();
            };
            AdminEventPopover.prototype.none = function () {
                return false;
            };
            AdminEventPopover.prototype.showPenalty = function () {
                this.$el.find(".role-penalty-form").toggle();
            };
            AdminEventPopover.prototype.penaltySave = function () {
                var self = this;
                var model = new App.Models.PenaltyModel();
                var penalty = {
                    eventId: this.model.id,
                    description: this.$el.find(".role-description").val(),
                    price: this.$el.find(".role-price").val()
                };
                model.save(penalty, {
                    success: function (item, response) {
                        self.remove();
                    }
                });
                return false;
            };
            AdminEventPopover.prototype.deleteEvent = function () {
                var self = this;
                var mm = new App.Models.EventModel(this.model);
                mm.destroy({
                    success: function (model, response) {
                        self.close();
                    }
                });
            };
            return AdminEventPopover;
        })(Backbone.View);
        Views.AdminEventPopover = AdminEventPopover;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var Tools = App.Tools;
var App;
(function (App) {
    var Views;
    (function (Views) {
        var ToturialModal = (function (_super) {
            __extends(ToturialModal, _super);
            function ToturialModal(model) {
                this.template = App.Tools.getTemplate("#toturial-view-template");
                this.src = "";
                this.events = {
                    "click .uk-modal": "close",
                    "click .uk-modal-close": "close",
                    "click .uk-modal-dialog": "none",
                    "click .role-close-dialog": "close"
                };
                _super.call(this, model);
            }
            ToturialModal.prototype.initialize = function (options) {
                this.src = options.src;
            };
            ToturialModal.prototype.render = function () {
                this.$el.html(this.template({ src: this.src }));
                return this;
            };
            ToturialModal.prototype.close = function () {
                this.remove();
                this.trigger("close", this.model);
            };
            ToturialModal.prototype.none = function () {
                return false;
            };
            return ToturialModal;
        })(Backbone.View);
        Views.ToturialModal = ToturialModal;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Views;
    (function (Views) {
        var Template = App.Tools.getTemplate;
        var WallModal = (function (_super) {
            __extends(WallModal, _super);
            function WallModal(model) {
                this.template = Template("#wall-popup-view-template");
                this.src = "";
                this.events = {
                    "click .uk-modal": "close",
                    "click .uk-modal-close": "close",
                    "click .uk-modal-dialog": "none",
                    "click .role-close-dialog": "close",
                    "click .clicl": "click",
                    "click .role-like": "like",
                    "click .role-dislike": "dislike",
                    "click .role-delete-wall-post": "deleteWall",
                    "click .role-comment-cancel": "cancelComment",
                    "click .role-comment-send": "sendComment",
                    "blur .role-comment-area": "hideControlls",
                    "focus .role-comment-area": "showControlls",
                    "click .uk-comment-body": "showFullViewPost",
                };
                _super.call(this, model);
            }
            WallModal.prototype.initialize = function (options) {
                this.src = options.src;
            };
            WallModal.prototype.render = function () {
                this.$el.html(this.template(this.model.toJSON()));
                return this;
            };
            WallModal.prototype.close = function () {
                this.remove();
            };
            WallModal.prototype.none = function () {
                return false;
            };
            return WallModal;
        })(Backbone.View);
        Views.WallModal = WallModal;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
;
/// <reference path="../../../typings/jquery/jquery.d.ts"/>
/// <reference path="../../../typings/jqueryui/jqueryui.d.ts"/>
/// <reference path="../../../typings/underscore/underscore.d.ts"/>
/// <reference path="../../../typings/backbone/backbone.d.ts"/>
/// <reference path="../../../typings/marionette/marionette.d.ts"/> 
/// <reference path="../../../typings/uikit/uikit.d.ts"/> 
/// <reference path="../../Tools.ts"/> 
var App;
(function (App) {
    var BaseViews;
    (function (BaseViews) {
        var BaseView = (function (_super) {
            __extends(BaseView, _super);
            function BaseView(options) {
                _super.call(this, options);
            }
            BaseView.prototype.changeInput = function (evt) {
                var changed = evt.currentTarget;
                var value = $(evt.currentTarget).val();
                this.model.set(this.toCamelCase(changed.id), value);
            };
            BaseView.prototype.updateCover = function (url) {
                this.model.save({ 'coverImage': url });
            };
            BaseView.prototype.updateAvatar = function (url) {
                this.model.save({ 'teaserImage': url });
            };
            BaseView.prototype.save = function () {
                this.model.save();
            };
            //success(model, resp, options) {
            //    $(".error").removeClass("error");
            //    $(".field-validation-valid").hide();
            //    UIkit.notify("Изменения успешно сохранены", { status: 'success', pos: 'bottom-right' });
            //}
            BaseView.prototype.savePhone = function () {
                var self = this;
                $("#modal-phone-required").hide();
                $.post("/vjson/Phones/SubmitPhone", {
                    ID: self.model.get("id"),
                    ConfirmCode: self.$el.find("[data-role-sms-verification]").val()
                }, function (data) {
                    if (typeof data.error == "undefined") {
                        UIkit.notify(data.message);
                    }
                    else {
                        UIkit.notify(data.message);
                    }
                });
            };
            BaseView.prototype.sendToValidate = function () {
                var self = this;
                $("#modal-phone-required").show();
                $.post("/vjson/Phones/ValidatePhone", { ID: self.model.get("id"), Number: self.$el.find("[data-role-phone-number]").val() }, function (data) {
                    UIkit.notify(data.message);
                });
            };
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
            BaseView.prototype.error = function (model, xhr, options) {
                var response = xhr.responseJSON;
                $(".error").removeClass('error');
                $(".field-validation-valid").hide();
                //$.UIkit.notify(response.message);
                UIkit.notify("Ошибка! " + response.message, { status: 'danger', pos: 'bottom-right' });
                $.each(response.modelState, function (key, value) {
                    $("#" + key.replace('model.', '')).addClass("error");
                    $.each(value, function () {
                        $("#" + key.replace("model.", '')).parent().find('span').html(this).show();
                    });
                });
            };
            BaseView.prototype.toCamelCase = function (str) {
                return str.toLowerCase().replace(/-(.)/g, function (match, group1) { return group1.toUpperCase(); });
            };
            BaseView.prototype.closeModal = function () {
                $("#modal-phone-required").hide();
            };
            BaseView.prototype.redirectToAdminPanel = function () {
                window.location.href = App.makeUrl("/Admin/Home");
            };
            return BaseView;
        })(Backbone.View);
        BaseViews.BaseView = BaseView;
        var BaseEditView = (function (_super) {
            __extends(BaseEditView, _super);
            function BaseEditView(options) {
                _super.call(this, options);
            }
            BaseEditView.prototype.success = function (model, resp, options) {
                this.model.set("id", resp.id);
                UIkit.notify(resp.message);
            };
            BaseEditView.prototype.deletePhone = function (e) {
                var self = this;
                var temp = $(e.currentTarget).parent().find("input").val();
                $.ajax({
                    url: App.makeUrl("/vjson/Phones/DeletePhone"),
                    method: "DELETE",
                    data: { ID: self.model.get("id"), Number: temp }
                }).done(function (data) {
                    if (typeof data.error == "undefined") {
                        UIkit.notify(data.message);
                        $(e.currentTarget).parent().parent().empty();
                    }
                    else {
                        UIkit.notify(data.message);
                    }
                });
            };
            return BaseEditView;
        })(BaseView);
        BaseViews.BaseEditView = BaseEditView;
        var BaseCreateView = (function (_super) {
            __extends(BaseCreateView, _super);
            function BaseCreateView() {
                _super.apply(this, arguments);
            }
            BaseCreateView.prototype.success = function (model, resp, options) {
                this.model.set("id", resp.id);
                this.$el.find("[data-role-menu]").find("li.uk-active").removeClass("uk-active").addClass("uk-success").next("li").removeClass('uk-disabled').addClass("uk-active");
                this.$el.find("[data-role-content]").find("li.uk-active").removeClass("uk-active").next("li").addClass("uk-active");
            };
            BaseCreateView.prototype.prev = function () {
                this.$el.find("[data-role-menu]").find("li.uk-active").removeClass("uk-active").prev("li").addClass("uk-active");
                this.$el.find("[data-role-content]").find("li.uk-active").removeClass("uk-active").prev("li").addClass("uk-active");
            };
            return BaseCreateView;
        })(BaseView);
        BaseViews.BaseCreateView = BaseCreateView;
    })(BaseViews = App.BaseViews || (App.BaseViews = {}));
})(App || (App = {}));
///<reference path="../BaseViews/AbstractView.ts"/>
var App;
(function (App) {
    var Views;
    (function (Views) {
        var PenaltyList = (function (_super) {
            __extends(PenaltyList, _super);
            function PenaltyList(model) {
                this.template = App.Tools.getTemplate("#penalties-list-view-template");
                this.events = {
                    "click .clicl": "click"
                };
                _super.call(this, model);
            }
            PenaltyList.prototype.initialize = function (options) { };
            PenaltyList.prototype.render = function () {
                var _this = this;
                this.setElement(this.template(this.model));
                $.each(this.model.penalties, function (index, value) {
                    _this.$el.find("#penalties-placeholder").append(new App.Views.PenaltySummary({ model: value }).render().el);
                });
                return this;
            };
            PenaltyList.prototype.click = function (event) { };
            return PenaltyList;
        })(Backbone.View);
        Views.PenaltyList = PenaltyList;
        var PenaltySummary = (function (_super) {
            __extends(PenaltySummary, _super);
            function PenaltySummary(model) {
                this.template = App.Tools.getTemplate("#penalty-summary-template");
                this.events = {
                    "click .clicl": "click"
                };
                _super.call(this, model);
            }
            PenaltySummary.prototype.initialize = function (options) { };
            PenaltySummary.prototype.render = function () {
                this.setElement(this.template(this.model));
                return this;
            };
            PenaltySummary.prototype.click = function (event) { };
            return PenaltySummary;
        })(Backbone.View);
        Views.PenaltySummary = PenaltySummary;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
///<reference path="../Models/CalendarModel.ts"/>
///<reference path="../Tools.ts"/>
///<reference path="../../typings/fullCalendar/fullCalendar.d.ts"/>
var App;
(function (App) {
    var Views;
    (function (Views) {
        var CalendarModel = App.Models.CalendarModel;
        var PhotomskCalendarOptions = (function () {
            function PhotomskCalendarOptions() {
            }
            return PhotomskCalendarOptions;
        })();
        Views.PhotomskCalendarOptions = PhotomskCalendarOptions;
        /*
        * Events:
        * eventAdded({event}); trigered when event added to calendar.
        *
        */
        var PhotomskCalendar = (function (_super) {
            __extends(PhotomskCalendar, _super);
            function PhotomskCalendar(model) {
                this.tmpEvent = null;
                this.template = App.Tools.getTemplate("#calendar-view-template");
                this.src = "";
                this.events = {
                    "click .role-close-dialog": "close",
                    "click .role-hall-tabs button": "changeCalendar"
                };
                _super.call(this, model);
            }
            PhotomskCalendar.prototype.initialize = function (options) {
                this.eventSources = this.getCalendars();
                this.options = options;
            };
            PhotomskCalendar.prototype.render = function () {
                var self = this;
                this.$el.html(this.template(this.model));
                if (this.model.halls.length > 1) {
                    var tmp = _.template($("#calendar-header-view-template").html());
                    this.$(".calendar-header-placeholder").append(tmp({ halls: this.model.halls }));
                }
                this.eventSources = this.getCalendars();
                this.calendar = this.$('.calendar-placeholder');
                this.calendar.fullCalendar($.extend(this.options.calendarOption || {}, {
                    defaultView: 'agendaWeek',
                    lang: "ru",
                    slotMinutes: 30,
                    allDaySlot: false,
                    selectable: true,
                    editable: true,
                    firstDay: 1,
                    select: function (start, end, allDay) {
                        var event = {
                            title: "Бронирование",
                            start: start,
                            end: end,
                            color: "red",
                            className: "role-add-booking"
                        };
                        self.tmpEvent = event;
                        self.addEvent();
                        self.calendar.fullCalendar('renderEvent', event, true);
                        self.calendar.fullCalendar('unselect');
                    },
                    eventClick: function (calEvent, jsEvent, view) {
                        self.calendarEventClick(calEvent, jsEvent, view);
                    },
                    eventDrop: function (event, delta, revertFunc) {
                        self.updateEvent(event, delta, revertFunc);
                    },
                    eventResize: function (event, delta, revertFunc) {
                        self.updateEvent(event, delta, revertFunc);
                    },
                    columnFormat: {
                        month: 'ddd',
                        week: '%D/% #ddd/#',
                        day: '(#)dddd(/#) M/d'
                    },
                    eventRender: function (event, element) {
                        if (event.title == 'online') {
                            element.find('.fc-title').html('<span class="uk-badge uk-badge-warning">Онлайн</span>');
                        }
                        if (event.tags != null && typeof (event.tags) != "undefined" && event.tags != '') {
                            var tags = event.tags.split(',');
                            _.each(tags, function (tag) {
                                element.find('.fc-content').append('<span class="uk-badge uk-badge-warning">' + tag + '</span>');
                            });
                        }
                    },
                    eventSources: self.eventSources
                }));
                return this;
            };
            PhotomskCalendar.prototype.getCalendars = function (id) {
                this.arr = [];
                var self = this;
                if (typeof id == 'undefined' || id == null) {
                    $.each(this.model.halls, function () {
                        var value = this;
                        self.arr.push({
                            id: value.calendarID,
                            url: App.makeUrl("/api/Events"),
                            type: 'GET',
                            data: {
                                calendarId: value.calendarID,
                            },
                            error: function () {
                                alert('there was an error while fetching events!');
                            },
                            color: value.color,
                            textColor: 'black' // a non-ajax option
                        });
                    });
                }
                else {
                    $.each(this.model, function () {
                        var value = this;
                        if (value.calendarID != id)
                            return;
                        self.arr.push({
                            id: value.calendarID,
                            url: App.makeUrl('/Api/Events'),
                            type: 'GET',
                            data: {
                                calendarId: value.calendarID,
                            },
                            error: function () {
                                alert('there was an error while fetching events!');
                            },
                            color: value.color,
                            textColor: 'black' // a non-ajax option
                        });
                    });
                }
                return self.arr;
            };
            PhotomskCalendar.prototype.changeCalendar = function (event) {
                var self = this;
                $(event.currentTarget).parent().parent().find(".uk-active").removeClass("uk-active");
                $(event.currentTarget).addClass("uk-active");
                var cid = $(event.currentTarget).attr("data-calendar");
                this.calendar.fullCalendar('removeEvents');
                $.each(this.eventSources, function () {
                    self.calendar.fullCalendar('removeEventSource', this);
                });
                this.eventSources = this.getCalendars(cid);
                $.each(this.eventSources, function () {
                    self.calendar.fullCalendar('addEventSource', this);
                });
                this.calendar.fullCalendar('refetchEvents');
            };
            PhotomskCalendar.prototype.calendarEventClick = function (calEvent, jsEvent, view) {
                var self = this;
                if (typeof this.popover != undefined)
                    this.popover.remove();
                var elem = new Views.AdminEventPopover({ model: calEvent }).render();
                elem.$el.css("top", jsEvent.pageY);
                elem.$el.css("left", jsEvent.pageX - 125);
                this.popover = elem;
                this.listenToOnce(this.popover, "close", function () {
                    self.calendar.fullCalendar('refetchEvents');
                });
                this.$el.append(elem.el);
            };
            PhotomskCalendar.prototype.updateEvent = function (event, delta, revertFunc) {
                var mmd = new App.Models.EventModel({
                    id: event.id
                });
                mmd.save({
                    start: event.start,
                    end: event.end
                });
            };
            PhotomskCalendar.prototype.addEvent = function () {
                if (this.model.halls.length > 1) {
                    var model = new CalendarModel();
                    this.listenToOnce(model, "sync", this.showCalendarChooser);
                    model.fetch({
                        data: { routeID: App.routeId, date: this.tmpEvent.start.format() }
                    });
                }
                else {
                    this.addToCart(this.model);
                }
            };
            PhotomskCalendar.prototype.showCalendarChooser = function (model, options) {
                var view = new Views.CalendarChooserModal({ model: model });
                this.listenToOnce(view, "save", this.addToCart);
                this.listenToOnce(view, "remove", this.remove);
                this.$el.append(view.render().el);
            };
            PhotomskCalendar.prototype.addToCart = function (arr) {
                var self = this;
                $.each(arr, function () {
                    var event = $.extend(true, {}, self.tmpEvent);
                    event.calendarID = this;
                    self.trigger('eventAdded', event);
                });
            };
            PhotomskCalendar.prototype.removeEvent = function (event) {
                this.tmpEvent = null;
            };
            PhotomskCalendar.prototype.reset = function () {
                var self = this;
                self.calendar.fullCalendar("refetchEvents");
                self.calendar.fullCalendar('removeEvents', function (event) {
                    return event.className == "role-add-booking";
                });
            };
            PhotomskCalendar.prototype.updateView = function () {
                this.calendar.fullCalendar('render');
            };
            return PhotomskCalendar;
        })(Backbone.View);
        Views.PhotomskCalendar = PhotomskCalendar;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
///<reference path="../SharedViews/BaseViews/AbstractView.ts"/>
var App;
(function (App) {
    var Views;
    (function (Views) {
        var UserInformation = (function (_super) {
            __extends(UserInformation, _super);
            function UserInformation(model) {
                this.template = App.Tools.getTemplate("#user-information-view-template");
                this.events = {
                    "click .clicl": "click"
                };
                _super.call(this, model);
            }
            UserInformation.prototype.initialize = function (options) { };
            UserInformation.prototype.render = function () {
                this.setElement(this.template(this.model));
                if (this.model.penalties != null && typeof this.model.penalties != 'undefined' && this.model.penalties.length > 0)
                    this.$el.find("#penalty-list-placeholder").html(new Views.PenaltyList({ model: this.model }).render().el);
                return this;
            };
            UserInformation.prototype.click = function (event) { };
            return UserInformation;
        })(Backbone.View);
        Views.UserInformation = UserInformation;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Views;
    (function (Views) {
        var UserInformationModel = App.Models.UserInformationModel;
        var CallHandlerModel = (function (_super) {
            __extends(CallHandlerModel, _super);
            function CallHandlerModel() {
                _super.apply(this, arguments);
            }
            return CallHandlerModel;
        })(Backbone.Model);
        Views.CallHandlerModel = CallHandlerModel;
        var UserEventCollection = (function (_super) {
            __extends(UserEventCollection, _super);
            function UserEventCollection(model) {
                this.url = App.makeUrl("/api/UserEvents");
                _super.call(this, model);
            }
            return UserEventCollection;
        })(Backbone.Collection);
        Views.UserEventCollection = UserEventCollection;
        ;
        var CallHandler = (function (_super) {
            __extends(CallHandler, _super);
            function CallHandler(model) {
                this.el = "body";
                this.eventCollection = new UserEventCollection();
                this.autoItem = App.Tools.getTemplate("#autocomplete-userinfo-view-template");
                this.newitem = App.Tools.getTemplate("#autocomplete-new-view-template");
                this.bookingTemplate = App.Tools.getTemplate("#booking-view-template");
                this.row = App.Tools.getTemplate("#event-row-view-template");
                this.newUser = false;
                this.popover = null;
                this.events = {
                    "focus .role-user-information-search": "autofocus",
                    "blur .role-user-information-search": "autoblur",
                    "click .booking-button": "booking",
                    "click .role-hall-tabs button": "changeCalendar",
                    "click .role-aditional-show": "showAditional"
                };
                _super.call(this, model);
            }
            CallHandler.prototype.initialize = function (options) {
                //  this.listenTo(this.eventCollection, "add", this.addEvent);
                this.validateEventCollection = new App.Collections.EventCollection();
                this.listenTo(this.validateEventCollection, "add", this.addValidatedEvent);
                this.userEventCollection = new UserEventCollection();
                this.listenTo(this.userEventCollection, "reset", this.userEventCollectionReset);
                if (this.model.userInformation != null) {
                    this.newUser = false;
                    this.userEventCollection.fetch({ data: { id: this.model.userInformation.id }, reset: true });
                    this.selectedUser = this.model.userInformation;
                }
                else
                    this.newUser = true;
            };
            CallHandler.prototype.render = function () {
                var self = this;
                $.each(this.model.photostudios, function () {
                    var cal = new App.Views.PhotomskCalendar({ model: this });
                    App.routeId = this.id;
                    self.$('#' + this.shortcut + "-calendar").append(cal.render().el);
                    self.listenTo(cal, 'eventAdded', self.eventAdded);
                    cal.updateView();
                });
                ///  $('.role-aditional-tags').tagit({});
                return this;
            };
            CallHandler.prototype.userEventCollectionReset = function () {
                var _this = this;
                $("#events").html('');
                this.userEventCollection.each(function (model) { return _this.userEventCollectionAdd(model); });
                return this;
            };
            CallHandler.prototype.userEventCollectionAdd = function (item) {
                $("#events").append(this.row(item.toJSON()));
            };
            CallHandler.prototype.eventAdded = function (event) {
                this.eventCollection.add(event);
                var self = this;
                $.get(App.makeUrl("/api/Events"), { validate: false, start: event.start.format(), end: event.end.format(), calendarIDs: [event.calendarID] }, function (data) {
                    if (data.error) {
                        UIkit.notify({
                            message: data.status,
                            status: 'danger',
                            timeout: 5000,
                            pos: 'bottom-right'
                        });
                        return;
                    }
                    $.each(data, function () {
                        self.validateEventCollection.add(this);
                    });
                });
            };
            CallHandler.prototype.addValidatedEvent = function (event) {
                this.$el.find("#booking").append(this.bookingTemplate(event.toJSON()));
                var price = 0;
                this.validateEventCollection.each(function (eevent) { return price += eevent.get("summ"); });
                this.$(".role-booking-total-price").html(App.Tools.formatMoney(price));
            };
            CallHandler.prototype.booking = function () {
                var self = this;
                if (this.newUser) {
                    this.selectedUser = new UserInformationModel({
                        firstName: $("#firstName").val(),
                        lastName: $("#lastName").val(),
                        UserPhone: self.model.phone,
                        admin: true
                    });
                }
                if (typeof self.selectedUser == 'undefined' || self.selectedUser == null) {
                    UIkit.notify({
                        message: 'Не введена инфорамция о пользователе',
                        status: 'danger',
                        timeout: 5000,
                        pos: 'top-center'
                    });
                    return;
                }
                if (this.eventCollection.length == 0) {
                    UIkit.notify({
                        message: 'Не выбрано время, бронируемое за человеком =)',
                        status: 'danger',
                        timeout: 5000,
                        pos: 'top-center'
                    });
                    return;
                }
                var model = new App.Models.BookingModel({ isAdmin: true, UserInformation: self.selectedUser, events: self.eventCollection.toJSON() });
                this.listenTo(model, "sync", this.successSync);
                this.listenTo(model, "error", this.errorSync);
                model.save();
            };
            CallHandler.prototype.addNewUserInfo = function () {
                this.newUser = true;
                this.$el.find("#client-placeholder")
                    .html($("#user-information-edit-template").html());
            };
            CallHandler.prototype.showAditional = function (event) {
                $('.role-aditional-form').show();
            };
            CallHandler.prototype.successSync = function () {
                var self = this;
                UIkit.notify({
                    message: 'Забронированно',
                    status: 'success',
                    timeout: 5000,
                    pos: 'top-center'
                });
                self.$("#client-placeholder").html('');
                self.$('.role-user-information-search').val('+375');
                self.$('.role-booking-total-price').html('0');
                self.$('#booking').html('');
                self.validateEventCollection.reset();
                self.eventCollection.reset();
                self.newUser = false;
                self.calendar.reset();
            };
            CallHandler.prototype.errorSync = function () {
                UIkit.notify({
                    message: 'Невозможно забронировать студию с такими параметрами, проверьте введенную информацию',
                    status: 'danger',
                    timeout: 5000,
                    pos: 'top-center'
                });
            };
            return CallHandler;
        })(Backbone.View);
        Views.CallHandler = CallHandler;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Views;
    (function (Views) {
        var FrameCalendar = (function (_super) {
            __extends(FrameCalendar, _super);
            function FrameCalendar(model) {
                this.el = "body";
                this.events = {
                    "click .role-hall-tabs button": "changeCalendar"
                };
                _super.call(this, model);
            }
            FrameCalendar.prototype.initialize = function (options) { };
            FrameCalendar.prototype.render = function () {
                var self = this;
                var tmp = _.template($("#calendar-header-view-template").html());
                $(".calendar-header-placeholder").append(tmp({ halls: this.model }));
                this.eventSources = this.getCalendars();
                this.calendar = $("#calendar").fullCalendar({
                    defaultView: 'agendaWeek',
                    slotMinutes: 30,
                    allDaySlot: false,
                    selectable: true,
                    lang: "ru",
                    editable: true,
                    firstDay: 1,
                    select: function (start, end, allDay) {
                        var event = new App.Models.EventModel({
                            title: "Бронирование",
                            start: start,
                            end: end,
                            color: "red"
                        });
                        self.eventCollection.add(event);
                        self.calendar.fullCalendar('renderEvent', event.toJSON(), true);
                        self.calendar.fullCalendar('unselect');
                    },
                    columnFormat: {
                        month: 'ddd',
                        week: '%D/% #ddd/#',
                        day: '(#)dddd(/#) M/d'
                    },
                    eventSources: self.eventSources
                });
                return this;
            };
            FrameCalendar.prototype.getCalendars = function (id) {
                this.arr = [];
                var self = this;
                if (typeof id == 'undefined' || id == null) {
                    $.each(this.model, function (index, value) {
                        self.arr.push({
                            id: value.id,
                            url: App.makeUrl('/Api/Events'),
                            type: 'GET',
                            data: {
                                calendarID: value.id,
                            },
                            error: function () {
                                alert('there was an error while fetching events!');
                            },
                            color: value.color,
                            textColor: 'black' // a non-ajax option
                        });
                    });
                }
                else {
                    $.each(this.model, function (index, value) {
                        if (value.id != id)
                            return;
                        self.arr.push({
                            id: value.id,
                            url: App.makeUrl('/Api/Events'),
                            type: 'GET',
                            data: {
                                calendarID: value.id,
                            },
                            error: function () {
                                alert('there was an error while fetching events!');
                            },
                            color: value.color,
                            textColor: 'black' // a non-ajax option
                        });
                    });
                }
                return self.arr;
            };
            FrameCalendar.prototype.changeCalendar = function (event) {
                var self = this;
                $(event.currentTarget).parent().parent().find(".uk-active").removeClass("uk-active");
                $(event.currentTarget).addClass("uk-active");
                var cid = $(event.currentTarget).attr("data-calendar");
                this.calendar.fullCalendar('removeEvents');
                $.each(this.eventSources, function () {
                    self.calendar.fullCalendar('removeEventSource', this);
                });
                this.eventSources = this.getCalendars(cid);
                $.each(this.eventSources, function () {
                    self.calendar.fullCalendar('addEventSource', this);
                });
                this.calendar.fullCalendar('refetchEvents');
            };
            return FrameCalendar;
        })(Backbone.View);
        Views.FrameCalendar = FrameCalendar;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Views;
    (function (Views) {
        var MainPage = (function (_super) {
            __extends(MainPage, _super);
            function MainPage(model) {
                this.el = "body";
                _super.call(this, model);
            }
            MainPage.prototype.initialize = function (options) { };
            MainPage.prototype.render = function () {
                this.mainWall = new App.Views.MainWallView();
                this.$("#wall-placeholder").html(this.mainWall.render().el);
                return this;
            };
            MainPage.prototype.reset = function (data) {
                this.mainWall.reset(data);
            };
            return MainPage;
        })(Backbone.View);
        Views.MainPage = MainPage;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Views;
    (function (Views) {
        var PageCategoryModel = (function (_super) {
            __extends(PageCategoryModel, _super);
            function PageCategoryModel() {
                _super.apply(this, arguments);
                this.urlRoot = App.makeUrl("/api/TextPageCategory");
            }
            return PageCategoryModel;
        })(Backbone.Model);
        Views.PageCategoryModel = PageCategoryModel;
        var MenuItemModel = (function (_super) {
            __extends(MenuItemModel, _super);
            function MenuItemModel() {
                _super.apply(this, arguments);
            }
            return MenuItemModel;
        })(Backbone.Model);
        Views.MenuItemModel = MenuItemModel;
        var ItemView = (function (_super) {
            __extends(ItemView, _super);
            function ItemView() {
                _super.apply(this, arguments);
            }
            ItemView.prototype.initialize = function () {
                this.template = _.template("<option id='<%- id %>' value='<%- id %>'><%- categoryName %></option>");
            };
            ItemView.prototype.render = function () {
                this.setElement(this.template(this.model.toJSON()));
                return this;
            };
            return ItemView;
        })(Backbone.View);
        Views.ItemView = ItemView;
        var PageCategories = (function (_super) {
            __extends(PageCategories, _super);
            function PageCategories(model) {
                this.url = App.makeUrl("/api/TextPageCategory");
                _super.call(this, model);
            }
            return PageCategories;
        })(Backbone.Collection);
        Views.PageCategories = PageCategories;
        var CollectionView = (function (_super) {
            __extends(CollectionView, _super);
            function CollectionView(model) {
                this.tagName = "select";
                this.events = {
                    "change": "selectItem"
                };
                _super.call(this, model);
            }
            CollectionView.prototype.initialize = function (options) {
                var routeId = options.routeId;
                this.collection = new PageCategories();
                this.collection.on('sync', this.render, this);
                this.collection.fetch({ data: { routeID: routeId } });
            };
            CollectionView.prototype.selectItem = function (event) {
                this.trigger("CategoryChanged", $(event.currentTarget).val());
            };
            CollectionView.prototype.render = function () {
                _.each(this.collection.models, function (item) {
                    this.$el.append(new ItemView({ model: item }).render().el);
                }, this);
                return this;
            };
            return CollectionView;
        })(Backbone.View);
        Views.CollectionView = CollectionView;
        ;
        var MenuEdit = (function (_super) {
            __extends(MenuEdit, _super);
            function MenuEdit(model) {
                this.el = "body";
                this.events = {
                    "click [data-role-save-button]": "save",
                    "change [data-role-input]": "changeInput",
                    "change [data-role-changePageType]": "changePageType"
                };
                _super.call(this, model);
            }
            MenuEdit.prototype.initialize = function (options) {
                var self = this;
                self.model = new MenuItemModel({ routeId: App.routeId });
                this.listenTo(this.model, "sync", this.success);
                this.listenTo(this.model, "error", this.error);
            };
            MenuEdit.prototype.render = function () {
                var self = this;
                var ac = $("input.uk-search-field").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: App.makeUrl("/Api/TextPage"),
                            dataType: "json",
                            data: {
                                routeID: App.routeId,
                                name: request.term
                            },
                            success: function (data) {
                                response($.map(data, function (item) {
                                    return {
                                        label: item.title,
                                        value: item.id
                                    };
                                }));
                            }
                        });
                    },
                    minLength: 3,
                    select: function (event, ui) {
                        self.model.set("textPageID", ui.item.value);
                    },
                    open: function () {
                        $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                    },
                    close: function () {
                        $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                    }
                });
                return this;
            };
            //ac._renderItem = function( ul, item ) {
            //    return $("<li>").html(item.label).appendTo( ul );
            // };
            //},
            MenuEdit.prototype.changePageType = function (evt) {
                var elem = $(evt.currentTarget).val();
                this.model.set("type", elem);
                switch (elem) {
                    case "PageMenuItem":
                        $("#LinkMenuItem").hide();
                        $("#PageMenuItem").show();
                        break;
                    case "LinkMenuItem":
                        $("#PageMenuItem").hide();
                        $("#LinkMenuItem").show();
                        break;
                    default:
                }
            };
            MenuEdit.prototype.save = function () {
                this.model.save();
            };
            return MenuEdit;
        })(App.BaseViews.BaseEditView);
        Views.MenuEdit = MenuEdit;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
///<reference path="../../SharedViews/BaseViews/AbstractView.ts"/>
///<reference path="../MenuItem/MenuItem.ts"/>
///<reference path="../../Application.ts"/>
var App;
(function (App) {
    var Views;
    (function (Views) {
        var TextPageModel = (function (_super) {
            __extends(TextPageModel, _super);
            function TextPageModel() {
                _super.apply(this, arguments);
            }
            return TextPageModel;
        })(Backbone.Model);
        Views.TextPageModel = TextPageModel;
        ;
        var TextPagesEditView = (function (_super) {
            __extends(TextPagesEditView, _super);
            function TextPagesEditView(model) {
                this.el = "body";
                this.events = {
                    "click [data-role-save-button]": "save",
                    "click [data-role-new-category]": "newCategory",
                    "click [data-role-save-category]": "saveCategory",
                    "change [data-role-input]": "changeInput",
                    "click [data-role-save-shortcut]": "saveShortcut"
                };
                _super.call(this, model);
            }
            TextPagesEditView.prototype.initialize = function (options) {
                var self = this;
                self.model = new TextPageModel({ routeId: App.routeId });
                this.listenTo(this.model, "sync", this.success);
                this.listenTo(this.model, "error", this.error);
            };
            TextPagesEditView.prototype.render = function () {
                this.categories = new Views.CollectionView({ routeId: App.routeId });
                $('[data-role-select-placeholder]').append(this.categories.render().el);
                this.listenTo(this.categories, "CategoryChanged", this.categoryChanged);
                return this;
            };
            TextPagesEditView.prototype.categoryChanged = function (arg) {
                this.model.set("pageCategoryId", arg);
            };
            TextPagesEditView.prototype.newCategory = function () {
                this.categoryModel = new Views.PageCategoryModel({ routeID: App.routeId });
                this.categoryModel.on("sync", this.categorySaved, this);
            };
            TextPagesEditView.prototype.saveCategory = function () {
                var vv = $("[data-role-category-name]").val();
                this.categoryModel.set("categoryName", vv);
                this.categoryModel.save();
            };
            TextPagesEditView.prototype.categorySaved = function (a, b, c) {
                this.categories.collection.add(this.categoryModel);
            };
            TextPagesEditView.prototype.saveShortcut = function () {
                this.model.set("slug", $("[data-role-shortcut]").val());
            };
            TextPagesEditView.prototype.save = function () {
                //   var text = $('.CodeMirror')[0].CodeMirror.getValue();
                // this.model.set("text", text);
                //  this.model.save();
            };
            return TextPagesEditView;
        })(App.BaseViews.BaseEditView);
        Views.TextPagesEditView = TextPagesEditView;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Views;
    (function (Views) {
        var PhototechnicSummary = (function (_super) {
            __extends(PhototechnicSummary, _super);
            function PhototechnicSummary(model) {
                this.vertical = App.Tools.getTemplate("#phototechnic-vertical-summary-template");
                this.horizontal = App.Tools.getTemplate("#phototechnic-horizontal-summary-template");
                this.search = App.Tools.getTemplate("#phototechnic-search-summary-template");
                this.tagName = "li";
                this.events = {
                    "click .role-show-calendar": "click",
                    "click [data-role-add-to-cart]": "addToCart"
                };
                _super.call(this, model);
            }
            PhototechnicSummary.prototype.initialize = function (options) {
                this.version = options.version;
                if (typeof options.className != 'undefined')
                    this.className += options.className;
            };
            PhototechnicSummary.prototype.render = function () {
                if (typeof (this.version) == 'undefined')
                    this.$el.html(this.vertical(this.model.toJSON()));
                else {
                }
                switch (this.version) {
                    case "vertical":
                        this.$el.html(this.vertical(this.model.toJSON()));
                        break;
                    case "horizontal":
                        this.$el.html(this.horizontal(this.model.toJSON()));
                        break;
                    case "search":
                        this.$el.html(this.search(this.model.toJSON()));
                        break;
                    default:
                        this.$el.html(this.vertical(this.model.toJSON()));
                }
                return this;
            };
            PhototechnicSummary.prototype.addToCart = function (event) {
                this.trigger("addToCart", this.model.get("id"));
            };
            PhototechnicSummary.prototype.click = function (event) {
                new App.Views.CalendarModal({ model: this.model }).render().$el.appendTo("body");
            };
            return PhototechnicSummary;
        })(Backbone.View);
        Views.PhototechnicSummary = PhototechnicSummary;
        var Phototrchnics = (function (_super) {
            __extends(Phototrchnics, _super);
            function Phototrchnics(model) {
                this.events = {};
                _super.call(this, model);
            }
            Phototrchnics.prototype.initialize = function (options) {
                this.phototechnics = new App.Collections.PhototechnicsCollection();
                this.listenTo(this.phototechnics, 'reset', this.addTechnics);
                this.listenTo(this.phototechnics, 'add', this.addTechnic);
            };
            Phototrchnics.prototype.render = function () {
                return this;
            };
            Phototrchnics.prototype.addTechnics = function () {
                this.phototechnics.each(this.addTechnic);
                return this;
            };
            Phototrchnics.prototype.addTechnic = function (model) {
                var view = new PhototechnicView({ model: model });
                $(".role-PhototechnicsViewModel-placeholder").append(view.render().el);
            };
            return Phototrchnics;
        })(Backbone.View);
        Views.Phototrchnics = Phototrchnics;
        var PhototechnicView = (function (_super) {
            __extends(PhototechnicView, _super);
            function PhototechnicView() {
                _super.apply(this, arguments);
            }
            //    template:_.template($("#PhototechnicView").html()),
            PhototechnicView.prototype.initialize = function (options) { };
            PhototechnicView.prototype.render = function () {
                return this;
            };
            PhototechnicView.prototype.click = function (event) { };
            return PhototechnicView;
        })(Backbone.View);
        Views.PhototechnicView = PhototechnicView;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Views;
    (function (Views) {
        var VideoModel = (function (_super) {
            __extends(VideoModel, _super);
            function VideoModel() {
                _super.apply(this, arguments);
            }
            return VideoModel;
        })(Backbone.Model);
        Views.VideoModel = VideoModel;
        var VideoUploadView = (function (_super) {
            __extends(VideoUploadView, _super);
            function VideoUploadView(model) {
                this.el = "body";
                this.template = App.Tools.getTemplate("#video-upload-view-template");
                this.className = "attachment";
                this.events = {
                    "change .VideoInput": "fileChanged",
                    "change .Description": "descriptionChanged",
                    "click .delete": "deleteModel",
                    "click .wall-post-save-video": "saveVideo",
                };
                _super.call(this, model);
            }
            VideoUploadView.prototype.initialize = function (parent) {
                this.model.set('type', "video");
            };
            VideoUploadView.prototype.render = function () {
                var template = $(this.template());
                this.$el.append(template);
                this.modal = new UIkit.modal('#' + template.attr("id"));
                this.modal.show();
                return this;
            };
            VideoUploadView.prototype.deleteModel = function () {
                this.$el.find(".attachment").hide();
                this.model.destroy();
            };
            VideoUploadView.prototype.fileChanged = function (event) {
                this.preview(this.$el.find(".VideoInput").val());
            };
            VideoUploadView.prototype.descriptionChanged = function (event) {
                this.model.set("title", this.$el.find(".Description").val());
            };
            VideoUploadView.prototype.preview = function (videoUri) {
                if ((!videoUri.match('http://*')) && (!videoUri.match('https://*'))) {
                    return;
                }
                if (videoUri.indexOf("youtu") != -1) {
                    var array = videoUri.split("?");
                    var array2 = array[1].split("=");
                    this.$el.find(".yuotube-video").attr("src", "https://www.youtube.com/embed/" + array2[1]);
                    this.model.set("url", "https://www.youtube.com/embed/" + array2[1]);
                    this.model.set("reference", "https://www.youtube.com/embed/" + array2[1]);
                    this.$el.find(".youtube").show();
                    this.trigger("added");
                }
                if (videoUri.indexOf("vimeo") != -1) {
                    var array = videoUri.split("com/");
                    this.$el.find(".vimeo-video").attr("src", " http://player.vimeo.com/video/" + array[1]);
                    this.model.set("url", "http://player.vimeo.com/video/" + array[1]);
                    this.model.set("reference", "http://player.vimeo.com/video/" + array[1]);
                    this.$el.find(".vimeo").show();
                    this.trigger("added");
                }
                // http://www.youtube.com/watch?v=AE0eTHdHPkQ
                // Specified dimentions are feasible for YouTube video only.
                // TODO: Process the input video URL in order to determine the video hosting and 
                // TODO: change video dimentions according to the video hosting.
                // TODO: For YouTube: width="420" height="315"
                // TODO: For Vimeo: width="500" height="281" 
                // videoUrl accepts URLs such as http://www.youtube.com/embed/yRhesBaRCME only.
                // TODO: Process the input video URL in order to determine the video hosting and 
                // TODO: change src value accordingly.
                // TODO: The following URL types should be accepted:
                // TODO: http://www.youtube.com/watch?v=yRhesBaRCME
                // TODO: http://youtu.be/yRhesBaRCME
                // TODO: http://youtu.be/yRhesBaRCME?t=1m1s
                // TODO: //www.youtube.com/embed/yRhesBaRCME
                // TODO: http://vimeo.com/61361236
                // TODO: //player.vimeo.com/video/61361236
            };
            VideoUploadView.prototype.saveVideo = function () {
                this.modal.hide();
            };
            return VideoUploadView;
        })(Backbone.View);
        Views.VideoUploadView = VideoUploadView;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Views;
    (function (Views) {
        var MainWallView = (function (_super) {
            __extends(MainWallView, _super);
            function MainWallView(model) {
                this.template = App.Tools.getTemplate("#main-wall-view-template");
                this.events = {};
                _super.call(this, model);
            }
            MainWallView.prototype.initialize = function (options) {
                this.wallCollection = new App.Collections.MainWallCollection();
                this.listenTo(this.wallCollection, 'reset', this.addWallPosts);
                this.listenTo(this.wallCollection, 'add', this.addWallPost);
            };
            MainWallView.prototype.render = function () {
                this.setElement(this.template());
                return this;
            };
            MainWallView.prototype.reset = function (data) {
                this.wallCollection.reset(data);
            };
            MainWallView.prototype.addWallPosts = function () {
                var _this = this;
                this.wallCollection.each(function (model) { return _this.addWallPost(model); });
                return this;
            };
            MainWallView.prototype.addWallPost = function (model) {
                var view = new Views.WallDayView();
                var el = view.render().el;
                this.$el.prepend(el);
                view.wallCollection.reset(model.get("items"));
            };
            return MainWallView;
        })(Backbone.View);
        Views.MainWallView = MainWallView;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Views;
    (function (Views) {
        var WallView = (function (_super) {
            __extends(WallView, _super);
            function WallView(model) {
                this.template = App.Tools.getTemplate("#wall-view-template");
                events: { }
                ;
                _super.call(this, model);
            }
            WallView.prototype.initialize = function (options) {
                this.wallCollection = new App.Collections.WallCollection();
                this.listenTo(this.wallCollection, 'reset', this.addWallPosts);
                this.listenTo(this.wallCollection, 'add', this.addWallPost);
            };
            WallView.prototype.render = function () {
                this.setElement(this.template());
                try {
                    if (typeof App.routeId !== "undefined") {
                        var view = new Views.WallEditView({ WallCollection: this.wallCollection });
                        this.$("#wall-edit-placeholder").html(view.render().el);
                    }
                }
                catch (exception) {
                    return this;
                }
                return this;
            };
            WallView.prototype.addWallPosts = function () {
                var _this = this;
                this.wallCollection.each(function (model) { return _this.addWallPost(model); });
                return this;
            };
            WallView.prototype.addWallPost = function (model) {
                var view = new Views.WallPost({ model: model });
                this.$("#wall").append(view.render().el);
            };
            return WallView;
        })(Backbone.View);
        Views.WallView = WallView;
        ;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Views;
    (function (Views) {
        var WallDayView = (function (_super) {
            __extends(WallDayView, _super);
            function WallDayView(model) {
                this.template = App.Tools.getTemplate("#wall-row-view-template");
                _super.call(this, model);
            }
            WallDayView.prototype.initialize = function (options) {
                this.wallCollection = options && typeof options.wallCollection != "undefined" ? options.wallCollection : new App.Collections.WallCollection();
                this.listenTo(this.wallCollection, "reset", this.addWallPosts);
                this.listenTo(this.wallCollection, "add", this.addWallPost);
            };
            WallDayView.prototype.render = function () {
                this.setElement(this.template());
                return this;
            };
            WallDayView.prototype.addWallPosts = function () {
                var _this = this;
                this.wallCollection.each(function (model) { return _this.addWallPost(model); });
                return this;
            };
            WallDayView.prototype.addWallPost = function (model) {
                var view = new Views.WallPost({ model: model });
                //this.$el.prepend($("<div class='uk-width-large-1-3 uk-width-medium-1-2 uk-width-small-1-1'></div>").append(view.render().el));
                view.render();
                this.$el.append(view.el);
            };
            return WallDayView;
        })(Backbone.View);
        Views.WallDayView = WallDayView;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Views;
    (function (Views) {
        var WallEditView = (function (_super) {
            __extends(WallEditView, _super);
            function WallEditView(model) {
                _super.call(this, model);
                this.template = App.Tools.getTemplate("#wall-edit-view-template");
                this.autoItem = App.Tools.getTemplate("#autocomplete-item-view-template");
                this.partsItem = App.Tools.getTemplate("#wall-participant-view-template");
                this.events = {
                    "click .role-save-wall-edit": "saveWall",
                    "click .role-shooting-edit": "shooting",
                    "click .role-post-edit": "posting",
                    "focus .role-wall-description": "showSave",
                    "blur .role-wall-description": "descriptionChanged",
                    "keyup .role-shooting-autocomplete": "addGenre",
                    "change .role-wall-video": "addVideo",
                    "click .role-toggle-video": "toggleVideo"
                };
                this.participants = [];
                this.attachments = [];
                this.genres = [];
                this.isshooting = false;
            }
            WallEditView.prototype.initialize = function (options) {
                this.wallCollection = options.WallCollection;
            };
            WallEditView.prototype.render = function () {
                var _this = this;
                this.setElement(this.template());
                var self = this;
                var auto = this.$el.find("#routes-autocomplete").autocomplete({
                    appendTo: this.$el.find(".role-wall-participant-autocomplete"),
                    source: function (request, respond) {
                        $.get(App.makeUrl("/api/Search/"), { search: request.term }, function (response) {
                            respond(response);
                        });
                    },
                    minLength: 2,
                    select: function (event, ui) {
                        _this.selectItem(ui.item);
                    }
                });
                auto.data("ui-autocomplete")._renderItem = function (ul, item) { return _this.renderAutocompleteItem(ul, item); };
                $(".ui-autocomplete").attr("class", "uk-nav uk-nav-autocomplete uk-autocomplete-results");
                var genres = this.$(".role-shooting-autocomplete").autocomplete({
                    source: function (request, respond) {
                        $.post("/vjson/Genre/FindGenres", { search: request.term }, function (response) {
                            respond(response);
                        });
                    },
                    minLength: 2,
                });
                this.progressbar = this.$el.find("#progressbar");
                this.bar = this.progressbar.find('.uk-progress-bar');
                var settings = {
                    action: $('#config-settings').attr('data-upload-url') + "/Api/Files",
                    single: false,
                    allow: '*.(jpg|jpeg|gif|png)',
                    loadstart: function () {
                        self.bar.css("width", "0%").text("0%");
                        self.progressbar.removeClass("uk-hidden");
                    },
                    progress: function (percent) {
                        percent = Math.ceil(percent);
                        self.bar.css("width", percent + "%").text(percent + "%");
                    },
                    allcomplete: function (response) {
                        self.filesUpload(response);
                    }
                };
                var select = new UIkit.uploadSelect(this.$el.find("#upload-select"), settings);
                var drop = new UIkit.uploadDrop(this.$el.find("#upload-drop"), settings);
                return this;
            };
            WallEditView.prototype.saveWall = function () {
                var self = this;
                var model = new App.Models.WallModel();
                if (typeof (App.routeId) == "undefined")
                    throw "owner id must be defined";
                var walldetalis = {
                    owner: App.routeId,
                    title: $(".role-wall-title").val(),
                    genres: self.genres,
                    shooting: self.isshooting,
                    description: $(".role-wall-description").val(),
                    routes: self.participants,
                    attachments: self.attachments
                };
                model.save(walldetalis, {
                    success: function (item, response) {
                        $("#wall-post-text").val("");
                        self.$(".role-wall-title").val('');
                        self.$(".role-wall-description").val('');
                        self.$(".wall-post-participants").html('');
                        self.participants = [];
                        self.$('.role-photo-placeholder').html('');
                        self.$('iframe').attr('src', '').hide();
                        self.attachments = [];
                        self.$('.role-shooting-genres').html('');
                        self.genres = [];
                        self.$('.role-show-when-post-edit').slideToggle();
                        self.wallCollection.add(response);
                    }
                });
            };
            WallEditView.prototype.renderAutocompleteItem = function (ul, item) {
                return $(this.autoItem(item)).appendTo(ul);
            };
            WallEditView.prototype.selectItem = function (item) {
                $(this.partsItem(item)).appendTo(".wall-post-participants");
                this.participants.push(item.id);
            };
            WallEditView.prototype.selectGenre = function (item) {
                if (this.genres.length < 3) {
                    this.genres.push(item);
                    this.$(".role-shooting-genres").append("<div>#" + item + "</div>");
                }
            };
            WallEditView.prototype.addVideo = function (event) {
                this.preview(this.$(".role-wall-video").val());
            };
            WallEditView.prototype.videoSaved = function (model) {
            };
            WallEditView.prototype.filesUpload = function (response) {
                var self = this;
                this.bar.css("width", "100%").text("100%");
                setTimeout(function () {
                    self.progressbar.addClass("uk-hidden");
                }, 250);
                var array = eval(response);
                $.each(array, function () {
                    var model = new App.Models.AttachmentModel(this);
                    model.set("type", "photo");
                    self.attachments.push(model);
                    $("<img></img>").attr("src", this.url).css("width", "75px").appendTo("#photo-placeholder");
                });
            };
            WallEditView.prototype.descriptionChanged = function (event) {
                this.model.set("description", this.$el.find(".Description").val());
            };
            WallEditView.prototype.preview = function (videoUri) {
                if ((!videoUri.match('http://*')) && (!videoUri.match('https://*'))) {
                    return;
                }
                var model = new App.Models.AttachmentModel();
                model.set('type', "video");
                if (videoUri.indexOf("youtu") != -1) {
                    var array = videoUri.split("?");
                    var array2 = array[1].split("=");
                    this.$el.find(".yuotube-video").attr("src", "https://www.youtube.com/embed/" + array2[1]);
                    model.set("url", "https://www.youtube.com/embed/" + array2[1]);
                    model.set("reference", "https://www.youtube.com/embed/" + array2[1]);
                    this.$el.find(".youtube").show();
                    this.trigger("added");
                }
                if (videoUri.indexOf("vimeo") != -1) {
                    var array = videoUri.split("com/");
                    this.$el.find(".vimeo-video").attr("src", " http://player.vimeo.com/video/" + array[1]);
                    model.set("url", "http://player.vimeo.com/video/" + array[1]);
                    model.set("reference", "http://player.vimeo.com/video/" + array[1]);
                    this.$el.find(".vimeo").show();
                    this.trigger("added");
                }
                // http://www.youtube.com/watch?v=AE0eTHdHPkQ
                // Specified dimentions are feasible for YouTube video only.
                // TODO: Process the input video URL in order to determine the video hosting and 
                // TODO: change video dimentions according to the video hosting.
                // TODO: For YouTube: width="420" height="315"
                // TODO: For Vimeo: width="500" height="281" 
                // videoUrl accepts URLs such as http://www.youtube.com/embed/yRhesBaRCME only.
                // TODO: Process the input video URL in order to determine the video hosting and 
                // TODO: change src value accordingly.
                // TODO: The following URL types should be accepted:
                // TODO: http://www.youtube.com/watch?v=yRhesBaRCME
                // TODO: http://youtu.be/yRhesBaRCME
                // TODO: http://youtu.be/yRhesBaRCME?t=1m1s
                // TODO: //www.youtube.com/embed/yRhesBaRCME
                // TODO: http://vimeo.com/61361236
                // TODO: //player.vimeo.com/video/61361236
                this.attachments.push(model);
            };
            WallEditView.prototype.shooting = function () {
                this.isshooting = true;
                $('.role-shooting-autocomplete').parent().show();
            };
            WallEditView.prototype.posting = function () {
                this.isshooting = false;
                $('.role-shooting-autocomplete').parent().hide();
            };
            WallEditView.prototype.showSave = function () {
                this.$el.find(".role-show-when-post-edit").slideDown();
            };
            WallEditView.prototype.addGenre = function (event) {
                if (event.keyCode == 13) {
                    var genre = this.$(".role-shooting-autocomplete").val();
                    this.$(".role-shooting-autocomplete").val('');
                    if (this.genres.length < 3) {
                        this.genres.push(genre);
                        this.$(".role-shooting-genres").append("<div>#" + genre + "</div>");
                    }
                }
            };
            WallEditView.prototype.toggleVideo = function (event) {
                this.$(".role-video-input-box").toggle();
            };
            return WallEditView;
        })(Backbone.View);
        Views.WallEditView = WallEditView;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Views;
    (function (Views) {
        var WallPost = (function (_super) {
            __extends(WallPost, _super);
            function WallPost(model) {
                this.template = App.Tools.getTemplate("#wall-post-view-template");
                this.tagName = "li";
                this.events = {
                    "click .role-like": "like",
                    "click .role-dislike": "dislike",
                    "click .role-delete-wall-post": "deleteWall",
                    "click .role-comment-cancel": "cancelComment",
                    "click .role-comment-send": "sendComment",
                    "blur .role-comment-area": "hideControlls",
                    "focus .role-comment-area": "showControlls",
                    "click .uk-comment-body": "showFullViewPost",
                };
                _super.call(this, model);
            }
            WallPost.prototype.initialize = function (options) { };
            WallPost.prototype.render = function () {
                var json = this.model.toJSON();
                this.$el.html(this.template(json));
                this.$(".role-comment-buttons").hide();
                return this;
            };
            WallPost.prototype.like = function (event) {
                var self = this;
                $.post("/vjson/like/post", {
                    postID: self.model.id,
                    likeType: 1
                }, function (data) {
                    self.reviewLike(data);
                });
            };
            WallPost.prototype.dislike = function (event) {
                var self = this;
                $.post("/vjson/like/post", {
                    PostID: self.model.id,
                    likeType: -1
                }, function (data) {
                    self.reviewLike(data);
                });
            };
            WallPost.prototype.reviewLike = function (data) {
                this.$el.find('.is-active').removeClass("is-active");
                this.$el.find('.role-like').find("b").html(data.likes);
                this.$el.find('.role-dislike').find("b").html(data.dislikes);
                if (data.opinion == 1) {
                    this.$el.find('.role-like').addClass('is-active');
                }
                else {
                    this.$el.find('.role-dislike').addClass('is-active');
                }
            };
            WallPost.prototype.deleteWall = function (event) {
                var self = this;
                $.post("/vjson/wallpost/delete", {
                    ID: self.model.id,
                }, function (data) {
                    self.remove();
                });
            };
            WallPost.prototype.cancelComment = function (event) {
            };
            WallPost.prototype.sendComment = function (event) {
                var self = this;
                var text = self.$el.find(".role-comment-area").val();
                $.post("/vjson/WallComment/post", {
                    ID: self.model.id,
                    text: text
                }, function (data) {
                    var view = _.template($('#comment-view-template').html());
                    var html = view(data);
                    var $html = $(html);
                    $html.hide();
                    self.$(".uk-comment-list").append($html);
                    self.$(".role-comment-area").val("");
                    $html.slideDown();
                    self.$(".role-comment-buttons").hide();
                });
            };
            WallPost.prototype.hideControlls = function (event) {
                this.$(".role-comment-buttons").slideToggle();
            };
            WallPost.prototype.showControlls = function (event) {
                this.$(".role-comment-buttons").slideDown();
            };
            WallPost.prototype.showFullViewPost = function (event) {
                new App.Views.WallModal({ model: this.model }).render().$el.appendTo("body");
            };
            return WallPost;
        })(Backbone.View);
        Views.WallPost = WallPost;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Collections;
    (function (Collections) {
        var MainWallCollection = (function (_super) {
            __extends(MainWallCollection, _super);
            function MainWallCollection(model) {
                this.url = App.makeUrl("/Api/MainWall");
                _super.call(this, model);
            }
            MainWallCollection.prototype.initialize = function (options) {
            };
            return MainWallCollection;
        })(Backbone.Collection);
        Collections.MainWallCollection = MainWallCollection;
        ;
    })(Collections = App.Collections || (App.Collections = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Models;
    (function (Models) {
        var EventModel = (function (_super) {
            __extends(EventModel, _super);
            function EventModel(model) {
                this.urlRoot = App.makeUrl("/Api/Events");
                _super.call(this, model);
            }
            return EventModel;
        })(Backbone.Model);
        Models.EventModel = EventModel;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Collections;
    (function (Collections) {
        var EventCollection = (function (_super) {
            __extends(EventCollection, _super);
            function EventCollection(model) {
                this.url = App.makeUrl("/Api/Events");
                _super.call(this, model);
            }
            EventCollection.prototype.initialize = function (options) {
            };
            return EventCollection;
        })(Backbone.Collection);
        Collections.EventCollection = EventCollection;
        var ModelsCollection = (function (_super) {
            __extends(ModelsCollection, _super);
            function ModelsCollection(model) {
                this.url = App.makeUrl("/Api/Photomodels");
                _super.call(this, model);
            }
            ModelsCollection.prototype.initialize = function (options) {
            };
            return ModelsCollection;
        })(Backbone.Collection);
        Collections.ModelsCollection = ModelsCollection;
    })(Collections = App.Collections || (App.Collections = {}));
})(App || (App = {}));
/// <reference path="../../typings/backbone/backbone.d.ts"/>
var App;
(function (App) {
    var Models;
    (function (Models) {
        var MainWallModel = (function (_super) {
            __extends(MainWallModel, _super);
            function MainWallModel() {
                _super.apply(this, arguments);
            }
            MainWallModel.prototype.defaults = function () {
                return {
                    title: ""
                };
            };
            MainWallModel.prototype.parse = function (data) {
                var response = {};
                return response;
            };
            return MainWallModel;
        })(Backbone.Model);
        Models.MainWallModel = MainWallModel;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
/// <reference path="../../../typings/backbone/backbone.d.ts"/>
var App;
(function (App) {
    var MasterclassModel = (function (_super) {
        __extends(MasterclassModel, _super);
        function MasterclassModel(model) {
            this.urlRoot = App.makeUrl("/api/masterclass");
            _super.call(this, model);
        }
        return MasterclassModel;
    })(Backbone.Model);
    App.MasterclassModel = MasterclassModel;
})(App || (App = {}));
/// <reference path="../../../typings/backbone/backbone.d.ts"/>
var App;
(function (App) {
    var PageModel = (function (_super) {
        __extends(PageModel, _super);
        function PageModel(model) {
            this.urlRoot = App.makeUrl("/api/page");
            _super.call(this, model);
        }
        return PageModel;
    })(Backbone.Model);
    App.PageModel = PageModel;
})(App || (App = {}));
/// <reference path="../../../typings/backbone/backbone.d.ts"/>
var App;
(function (App) {
    var PhotographerModel = (function (_super) {
        __extends(PhotographerModel, _super);
        function PhotographerModel(model) {
            this.urlRoot = App.makeUrl("/api/Photographer");
            _super.call(this, model);
        }
        return PhotographerModel;
    })(Backbone.Model);
    App.PhotographerModel = PhotographerModel;
})(App || (App = {}));
/// <reference path="../../../typings/backbone/backbone.d.ts"/>
var App;
(function (App) {
    var PhotoModelModel = (function (_super) {
        __extends(PhotoModelModel, _super);
        function PhotoModelModel(model) {
            this.urlRoot = App.makeUrl("/Api/photomodel");
            _super.call(this, model);
        }
        return PhotoModelModel;
    })(Backbone.Model);
    App.PhotoModelModel = PhotoModelModel;
})(App || (App = {}));
/// <reference path="../../../typings/backbone/backbone.d.ts"/>
var App;
(function (App) {
    var Models;
    (function (Models) {
        var PageView = (function (_super) {
            __extends(PageView, _super);
            function PageView() {
                _super.apply(this, arguments);
            }
            return PageView;
        })(Backbone.Model);
        Models.PageView = PageView;
        var PhotorentModel = (function (_super) {
            __extends(PhotorentModel, _super);
            function PhotorentModel(model) {
                this.urlRoot = App.makeUrl("/Api/Photorent");
                _super.call(this, model);
            }
            return PhotorentModel;
        })(Backbone.Model);
        Models.PhotorentModel = PhotorentModel;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
/// <reference path="../../../typings/backbone/backbone.d.ts"/>
var App;
(function (App) {
    var PhotoshopModel = (function (_super) {
        __extends(PhotoshopModel, _super);
        function PhotoshopModel(model) {
            this.urlRoot = App.makeUrl("/api/photoshop");
            _super.call(this, model);
        }
        return PhotoshopModel;
    })(Backbone.Model);
    App.PhotoshopModel = PhotoshopModel;
})(App || (App = {}));
/// <reference path="../../../typings/backbone/backbone.d.ts"/>
var App;
(function (App) {
    var PhotostudioModel = (function (_super) {
        __extends(PhotostudioModel, _super);
        function PhotostudioModel(model) {
            this.urlRoot = App.makeUrl("/api/photostudio");
            _super.call(this, model);
        }
        return PhotostudioModel;
    })(App.Models.CalendarModel);
    App.PhotostudioModel = PhotostudioModel;
})(App || (App = {}));
///<reference path="../../SharedViews/BaseViews/AbstractView.ts"/>
/// <reference path="../../../typings/croppic/Croppic.d.ts"/>
var App;
(function (App) {
    var Halls;
    (function (Halls) {
        var HallModel = (function (_super) {
            __extends(HallModel, _super);
            function HallModel() {
                _super.apply(this, arguments);
            }
            return HallModel;
        })(Backbone.Model);
        Halls.HallModel = HallModel;
        var HallCreate = (function (_super) {
            __extends(HallCreate, _super);
            function HallCreate(model) {
                this.el = "body";
                this.routeId = null;
                this.events = {
                    "change .input-ms": "changeInput",
                    "click .role-save-button": "save",
                };
                _super.call(this, model);
            }
            HallCreate.prototype.initialize = function (options) {
                this.model = new HallModel({ photostudioID: options.photostudioID });
                this.listenTo(this.model, "sync", this.success);
                this.listenTo(this.model, "error", this.error);
            };
            HallCreate.prototype.render = function () {
                return this;
            };
            HallCreate.prototype.success = function (data) {
                this.routeId = data.id;
                $('.error').removeClass('error');
                this.$el.find(".role-step-1").hide();
                this.$el.find(".role-step-2").show();
            };
            return HallCreate;
        })(App.BaseViews.BaseCreateView);
        Halls.HallCreate = HallCreate;
        ;
        var HallEdit = (function (_super) {
            __extends(HallEdit, _super);
            function HallEdit(model) {
                this.el = "body";
                this.events = {
                    "change .input-ms": "changeInput",
                    "click .role-save-button": "save"
                };
                _super.call(this, model);
            }
            HallEdit.prototype.initialize = function (options) {
                var self = this;
                var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
                var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";
                var cropCover = new Croppic('role-cover', {
                    customUploadButtonId: "role-change-cover",
                    cropUrl: curl,
                    uploadUrl: url,
                    onAfterImgCrop: function (data) {
                        self.updateCover(data.url);
                    },
                    imgEyecandy: true,
                    imgEyecandyOpacity: 0.2,
                    cropData: {
                        "zoomFactor": 2,
                    }
                });
                var cropAvatar = new Croppic('role-avatar', {
                    customUploadButtonId: "role-change-avatar",
                    cropUrl: curl,
                    modal: true,
                    uploadUrl: url,
                    width: 400,
                    height: 400,
                    onAfterImgCrop: function (data) {
                        self.updateAvatar(data.url);
                    }
                });
            };
            HallEdit.prototype.render = function () {
                return this;
            };
            return HallEdit;
        })(App.BaseViews.BaseEditView);
        Halls.HallEdit = HallEdit;
        ;
    })(Halls = App.Halls || (App.Halls = {}));
})(App || (App = {}));
///<reference path="../../SharedViews/BaseViews/AbstractView.ts"/>
/// <reference path="../../../typings/croppic/Croppic.d.ts"/>
///<reference path="../../Models/Routes/MasterclassModel.ts"/>
var App;
(function (App) {
    var Masterclass;
    (function (Masterclass) {
        var CreateView = (function (_super) {
            __extends(CreateView, _super);
            function CreateView(model) {
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
                _super.call(this, model);
            }
            CreateView.prototype.initialize = function (options) {
                var model = this.model = new App.MasterclassModel();
                var self = this;
                this.listenTo(model, "sync", this.success);
                this.listenTo(model, "error", this.error);
                var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
                var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";
                var cropAvatar = new Croppic('role-avatar', {
                    customUploadButtonId: "role-change-avatar",
                    cropUrl: curl,
                    modal: true,
                    width: 400,
                    height: 400,
                    uploadUrl: url,
                    onAfterImgCrop: function (data) {
                        model.set("TeaserImage", data.url);
                    }
                });
                var cropCover = new Croppic('role-cover', {
                    customUploadButtonId: "role-change-cover",
                    cropUrl: curl,
                    uploadUrl: url,
                    onAfterImgCrop: function (data) {
                        model.set("CoverImage", data.url);
                    },
                    imgEyecandy: true,
                    imgEyecandyOpacity: 0.2,
                });
                ymaps.ready(function () {
                    var myMap;
                    myMap = new ymaps.Map("map", {
                        center: [55.76, 37.64],
                        zoom: 9
                    });
                    myMap.events.add('click', function (e) {
                        // To get the geographical coordinates of the point of click,
                        // call .get('coordPosition')
                        var position = e.get('coords');
                        if (typeof (self.placemark) != 'undefined')
                            self.myMap.geoObjects.remove(self.placemark);
                        self.placemark = new ymaps.Placemark(position);
                        self.model.set("latitude", position[0]);
                        self.model.set("longitude", position[1]);
                        self.myMap.geoObjects.add(self.placemark);
                    });
                });
                this.$el.find("[data-role-menu]").find("li").not('.uk-active').addClass("uk-disabled");
            };
            CreateView.prototype.render = function () {
                this.setElement($("body"));
                this.delegateEvents();
                return this;
            };
            CreateView.prototype.checkRequired = function (e) {
                this.changeInput(e);
                if (this.model.has('title') && this.model.has('shortcut')) {
                    this.$el.find('button.uk-button-next-step').removeAttr("disabled");
                }
            };
            return CreateView;
        })(App.BaseViews.BaseCreateView);
        Masterclass.CreateView = CreateView;
        var EditView = (function (_super) {
            __extends(EditView, _super);
            function EditView(model) {
                this.events = {
                    "change [data-role-input-optional]": "changeInput",
                    "change [data-role-input-required]": "changeInput",
                    "click [data-role-close-modal]": "closeModal",
                    "click [data-role-save]": "save",
                    "click [data-role-save-phone]": "savePhone",
                    "click [data-role-send-to-validate]": "sendToValidate",
                    "click [data-role-to-adminpanel]": "redirectToAdminPanel",
                    "click [data-role-phone-delete]": "deletePhone",
                };
                _super.call(this, model);
            }
            EditView.prototype.initialize = function (options) {
                var self = this;
                self.model = new App.MasterclassModel({ id: options.routeId });
                this.listenTo(this.model, "sync", this.success);
                this.listenTo(this.model, "error", this.error);
                var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
                var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";
                var cropCover = new Croppic('role-cover', {
                    customUploadButtonId: "role-change-cover",
                    cropUrl: curl,
                    uploadUrl: url,
                    onAfterImgCrop: function (data) {
                        self.model.set("CoverImage", data.url);
                    },
                    imgEyecandy: true,
                    imgEyecandyOpacity: 0.2,
                    cropData: {
                        "zoomFactor": 2,
                    }
                });
                var cropAvatar = new Croppic('role-avatar', {
                    customUploadButtonId: "role-change-avatar",
                    cropUrl: curl,
                    modal: true,
                    uploadUrl: url,
                    width: 400,
                    height: 400,
                    onAfterImgCrop: function (data) {
                        self.model.set("TeaserImage", data.url);
                    }
                });
            };
            EditView.prototype.render = function () {
                this.setElement($("body"));
                this.delegateEvents();
                return this;
            };
            EditView.prototype.checkRequired = function (e) {
                if (this.model.has('firstname') && this.model.has('lastname') && this.model.has('shortcut')) {
                    this.$el.find('button.uk-button-next-step').removeAttr("disabled");
                }
            };
            return EditView;
        })(App.BaseViews.BaseEditView);
        Masterclass.EditView = EditView;
        var MasterclassDetails = (function (_super) {
            __extends(MasterclassDetails, _super);
            function MasterclassDetails(model) {
                this.el = "body";
                this.bookingTemplate = App.Tools.getTemplate("#booking-view-template");
                this.events = {
                    "click .role-hall-tabs button": "changeCalendar",
                    "click .booking-button": "booking",
                    "click .role-clean-data": "cleanData",
                    "click .role-booking-toturial": "showBookingToturial"
                };
                _super.call(this, model);
            }
            MasterclassDetails.prototype.initialize = function (options) {
                var self = this;
            };
            MasterclassDetails.prototype.render = function () {
                var view = new App.Views.WallView();
                this.$("#wall-placeholder").html(view.render().el);
                return this;
            };
            return MasterclassDetails;
        })(Backbone.View);
        Masterclass.MasterclassDetails = MasterclassDetails;
    })(Masterclass = App.Masterclass || (App.Masterclass = {}));
})(App || (App = {}));
///<reference path="../../SharedViews/BaseViews/AbstractView.ts"/>
/// <reference path="../../../typings/croppic/Croppic.d.ts"/>
///<reference path="../../Models/Routes/PageModel.ts"/>
var App;
(function (App) {
    var Page;
    (function (Page) {
        var CreateView = (function (_super) {
            __extends(CreateView, _super);
            function CreateView(model) {
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
                _super.call(this, model);
            }
            CreateView.prototype.initialize = function (options) {
                var model = this.model = new App.PageModel();
                this.listenTo(model, "sync", this.success);
                this.listenTo(model, "error", this.error);
                var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
                var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";
                var cropAvatar = new Croppic('role-avatar', {
                    customUploadButtonId: "role-change-avatar",
                    cropUrl: curl,
                    modal: true,
                    width: 400,
                    height: 400,
                    uploadUrl: url,
                    onAfterImgCrop: function (data) {
                        model.set("TeaserImage", data.url);
                    }
                });
                var cropCover = new Croppic('role-cover', {
                    customUploadButtonId: "role-change-cover",
                    cropUrl: curl,
                    uploadUrl: url,
                    onAfterImgCrop: function (data) {
                        model.set("CoverImage", data.url);
                    },
                    imgEyecandy: true,
                    imgEyecandyOpacity: 0.2,
                });
                this.$el.find("[data-role-menu]").find("li").not('.uk-active').addClass("uk-disabled");
            };
            CreateView.prototype.render = function () {
                this.setElement($("body"));
                this.delegateEvents();
                return this;
            };
            CreateView.prototype.checkRequired = function (e) {
                this.changeInput(e);
                if (this.model.has('name') && this.model.has('shortcut')) {
                    this.$el.find('button.uk-button-next-step').removeAttr("disabled");
                }
            };
            return CreateView;
        })(App.BaseViews.BaseCreateView);
        Page.CreateView = CreateView;
        var EditView = (function (_super) {
            __extends(EditView, _super);
            function EditView(model) {
                this.events = {
                    "change [data-role-input-optional]": "changeInput",
                    "change [data-role-input-required]": "changeInput",
                    "click [data-role-close-modal]": "closeModal",
                    "click [data-role-save]": "save",
                    "click [data-role-save-phone]": "savePhone",
                    "click [data-role-send-to-validate]": "sendToValidate",
                    "click [data-role-to-adminpanel]": "redirectToAdminPanel",
                    "click [data-role-phone-delete]": "deletePhone",
                };
                _super.call(this, model);
            }
            EditView.prototype.initialize = function (options) {
                var self = this;
                self.model = new App.PageModel({ id: options.routeId });
                this.listenTo(this.model, "sync", this.success);
                this.listenTo(this.model, "error", this.error);
                var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
                var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";
                var cropCover = new Croppic('role-cover', {
                    customUploadButtonId: "role-change-cover",
                    cropUrl: curl,
                    uploadUrl: url,
                    onAfterImgCrop: function (data) {
                        self.model.set("CoverImage", data.url);
                    },
                    imgEyecandy: true,
                    imgEyecandyOpacity: 0.2,
                    cropData: {
                        "zoomFactor": 2,
                    }
                });
                var cropAvatar = new Croppic('role-avatar', {
                    customUploadButtonId: "role-change-avatar",
                    cropUrl: curl,
                    modal: true,
                    uploadUrl: url,
                    width: 400,
                    height: 400,
                    onAfterImgCrop: function (data) {
                        self.model.set("TeaserImage", data.url);
                    }
                });
            };
            EditView.prototype.render = function () {
                this.setElement($("body"));
                this.delegateEvents();
                return this;
            };
            return EditView;
        })(App.BaseViews.BaseEditView);
        Page.EditView = EditView;
        var PublicDetails = (function (_super) {
            __extends(PublicDetails, _super);
            function PublicDetails(model) {
                this.el = "body";
                this.bookingTemplate = App.Tools.getTemplate("#booking-view-template");
                this.events = {
                    "click .role-booking-toturial": "showBookingToturial",
                    "click [data-role-show-callback]": "showclaback",
                    "click [data-role-send-request]": "sendRequest"
                };
                _super.call(this, model);
            }
            PublicDetails.prototype.initialize = function (options) {
                var self = this;
                //this.eventSources = this.getCalendars();
                this.eventCollection = new App.Collections.EventCollection(),
                    this.validateEventCollection = new App.Collections.EventCollection();
                //  this.listenTo(this.eventCollection, "add", this.addEvent);
            };
            PublicDetails.prototype.render = function () {
                var view = new App.Views.WallView();
                this.$("#wall-placeholder").html(view.render().el);
                // app.WallView = view;
                return this;
            };
            PublicDetails.prototype.sendRequest = function () {
                //var client = new $.RestClient("/api/");
                //client.add("callback");
                //client.callback.create({ routeID: app.routeId, phone: $("[data-role-pohone-number]").val() });
                //var modal = UIkit.modal("[data-role-dialog]");
                //modal.hide();
            };
            return PublicDetails;
        })(Backbone.View);
        Page.PublicDetails = PublicDetails;
    })(Page = App.Page || (App.Page = {}));
})(App || (App = {}));
///<reference path="../../SharedViews/BaseViews/AbstractView.ts"/>
/// <reference path="../../../typings/croppic/Croppic.d.ts"/>
/// <reference path="../../../typings/photosetGrid/photosetGrid.d.ts"/>
///<reference path="../../Models/Routes/PhotographerModel.ts"/>
var App;
(function (App) {
    var Photographer;
    (function (Photographer) {
        var CreateView = (function (_super) {
            __extends(CreateView, _super);
            function CreateView(model) {
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
                _super.call(this, model);
            }
            CreateView.prototype.initialize = function (options) {
                var model = this.model = new App.PhotographerModel();
                this.listenTo(model, "sync", this.success);
                this.listenTo(model, "error", this.error);
                var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
                var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";
                var cropAvatar = new Croppic('role-avatar', {
                    customUploadButtonId: "role-change-avatar",
                    cropUrl: curl,
                    modal: true,
                    width: 400,
                    height: 400,
                    uploadUrl: url,
                    onAfterImgCrop: function (data) {
                        model.set("TeaserImage", data.url);
                    }
                });
                var cropCover = new Croppic('role-cover', {
                    customUploadButtonId: "role-change-cover",
                    cropUrl: curl,
                    uploadUrl: url,
                    onAfterImgCrop: function (data) {
                        model.set("CoverImage", data.url);
                    },
                    imgEyecandy: true,
                    imgEyecandyOpacity: 0.2,
                });
                this.$el.find("[data-role-menu]").find("li").not('.uk-active').addClass("uk-disabled");
            };
            CreateView.prototype.render = function () {
                this.setElement($("body"));
                this.delegateEvents();
                return this;
            };
            CreateView.prototype.redirectToEdit = function () {
                window.location.href = App.makeUrl("/Admin/Photographer/Edit?shortcut=" + this.model.get("shortcut"));
            };
            CreateView.prototype.redirectToMyPage = function () {
                window.location.href = App.makeUrl("/Photographer/Details?shortcut=" + this.model.get("shortcut"));
            };
            CreateView.prototype.checkRequired = function (e) {
                this.changeInput(e);
                if (this.model.has('firstname') && this.model.has('lastname') && this.model.has('shortcut')) {
                    this.$el.find('button.uk-button-next-step').removeAttr("disabled");
                }
            };
            return CreateView;
        })(App.BaseViews.BaseCreateView);
        Photographer.CreateView = CreateView;
        var EditView = (function (_super) {
            __extends(EditView, _super);
            function EditView(model) {
                this.events = {
                    "change [data-role-input-optional]": "changeInput",
                    "change [data-role-input-required]": "changeInput",
                    "click [data-role-close-modal]": "closeModal",
                    "click [data-role-save]": "save",
                    "click [data-role-save-phone]": "savePhone",
                    "click [data-role-send-to-validate]": "sendToValidate",
                    "click [data-role-to-adminpanel]": "redirectToAdminPanel",
                    "click [data-role-phone-delete]": "deletePhone",
                };
                _super.call(this, model);
            }
            EditView.prototype.initialize = function (options) {
                var self = this;
                self.model = new App.PhotographerModel({ id: options.id });
                this.listenTo(this.model, "sync", this.success);
                this.listenTo(this.model, "error", this.error);
                var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
                var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";
                var cropCover = new Croppic('role-cover', {
                    customUploadButtonId: "role-change-cover",
                    cropUrl: curl,
                    uploadUrl: url,
                    onAfterImgCrop: function (data) {
                        self.model.set("CoverImage", data.url);
                    },
                    imgEyecandy: true,
                    imgEyecandyOpacity: 0.2,
                    cropData: {
                        "zoomFactor": 2,
                    }
                });
                var cropAvatar = new Croppic('role-avatar', {
                    customUploadButtonId: "role-change-avatar",
                    cropUrl: curl,
                    modal: true,
                    uploadUrl: url,
                    width: 400,
                    height: 400,
                    onAfterImgCrop: function (data) {
                        self.model.set("TeaserImage", data.url);
                    }
                });
            };
            EditView.prototype.render = function () {
                this.setElement($("body"));
                this.delegateEvents();
                return this;
            };
            return EditView;
        })(App.BaseViews.BaseEditView);
        Photographer.EditView = EditView;
        var PhotographerDetails = (function (_super) {
            __extends(PhotographerDetails, _super);
            function PhotographerDetails(model) {
                this.el = "body";
                this.bookingTemplate = App.Tools.getTemplate("#booking-view-template");
                this.events = {
                    "click .role-switch-tabs a": "rebuildGrid"
                };
                _super.call(this, model);
            }
            PhotographerDetails.prototype.initialize = function (options) { };
            PhotographerDetails.prototype.render = function () {
                var view = new App.Views.WallView();
                this.$("#wall-placeholder").html(view.render().el);
                App.WallView = view;
                return this;
            };
            PhotographerDetails.prototype.rebuildGrid = function () {
                $('.photoset-grid-basic').each(function () {
                    $(this).photosetGrid({
                        highresLinks: true,
                        rel: App.Tools.guid(),
                        gutter: '5px',
                        onComplete: function () {
                            $('.photoset-grid-basic a').colorbox({
                                photo: true,
                                scalePhotos: true,
                                maxHeight: '90%',
                                maxWidth: '90%'
                            });
                        }
                    });
                });
            };
            return PhotographerDetails;
        })(Backbone.View);
        Photographer.PhotographerDetails = PhotographerDetails;
        var PhotographerIndex = (function (_super) {
            __extends(PhotographerIndex, _super);
            function PhotographerIndex(model) {
                this.element = 'body';
                this.events = {};
                _super.call(this, model);
            }
            PhotographerIndex.prototype.initialize = function (options) {
                var _this = this;
                this.Photographers = new App.Collections.PhotographersCollection();
                this.listenTo(this.Photographers, "reset", this.addPhotographers);
                this.listenTo(this.Photographers, "add", this.addPhotographer);
                $('[data-uk-pagination]').on('uk-select-page', function (e, pageIndex) {
                    return _this.loadPage(e, pageIndex);
                });
            };
            PhotographerIndex.prototype.render = function () {
                return this;
            };
            PhotographerIndex.prototype.addPhotographers = function () {
                $(".role-photographers").html("");
                this.Photographers.each(this.addPhotographer);
                return this;
            };
            PhotographerIndex.prototype.addPhotographer = function (model) {
                var view = new PhotographerView({ model: model });
                $(".role-photographers").append(view.render().el);
            };
            PhotographerIndex.prototype.loadPage = function (e, pageIndex) {
                this.Photographers.fetch({
                    data: {
                        from: pageIndex * 20,
                        pageSize: 20
                    },
                    reset: true
                });
            };
            return PhotographerIndex;
        })(Backbone.View);
        Photographer.PhotographerIndex = PhotographerIndex;
        var PhotographerView = (function (_super) {
            __extends(PhotographerView, _super);
            function PhotographerView(model) {
                this.template = App.Tools.getTemplate("#photographer-summary-template");
                this.events = {
                    "click .clicl": "click"
                };
                _super.call(this, model);
            }
            PhotographerView.prototype.initialize = function (options) { };
            PhotographerView.prototype.render = function () {
                this.setElement(this.template(this.model.toJSON()));
                return this;
            };
            PhotographerView.prototype.click = function (event) { };
            return PhotographerView;
        })(Backbone.View);
        Photographer.PhotographerView = PhotographerView;
    })(Photographer = App.Photographer || (App.Photographer = {}));
})(App || (App = {}));
///<reference path="../../SharedViews/BaseViews/AbstractView.ts"/>
var App;
(function (App) {
    var Views;
    (function (Views) {
        var CreateView = (function (_super) {
            __extends(CreateView, _super);
            function CreateView(model) {
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
                _super.call(this, model);
            }
            CreateView.prototype.render = function () {
                this.setElement($("body"));
                this.delegateEvents();
                return this;
            };
            CreateView.prototype.initialize = function (options) {
                var model = this.model = new App.PhotoModelModel();
                this.listenTo(model, "sync", this.success);
                this.listenTo(model, "error", this.error);
                var self = this;
                var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
                var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";
                var cropAvatar = new Croppic('role-avatar', {
                    customUploadButtonId: "role-change-avatar",
                    cropUrl: curl,
                    modal: true,
                    width: 400,
                    height: 400,
                    uploadUrl: url,
                    onAfterImgCrop: function (data) {
                        model.set("TeaserImage", data.url);
                    }
                });
                ymaps.ready(function () {
                    self.myMap = new ymaps.Map("map", { center: [55.76, 37.64], zoom: 9 });
                    self.myMap.events.add('click', function (e) {
                        // To get the geographical coordinates of the point of click,
                        // call .get('coordPosition')
                        var position = e.get('coords');
                        if (typeof (self.placemark) != 'undefined')
                            self.myMap.geoObjects.remove(self.placemark);
                        self.placemark = new ymaps.Placemark(position);
                        self.model.set("latitude", position[0]);
                        self.model.set("longitude", position[1]);
                        self.myMap.geoObjects.add(self.placemark);
                    });
                });
                this.$el.find("[data-role-menu]").find("li").not('.uk-active').addClass("uk-disabled");
            };
            CreateView.prototype.redirectToEdit = function () {
                window.location.href = App.makeUrl("/Admin/Photomodel/Edit?shortcut=" + this.model.get("shortcut"));
            };
            CreateView.prototype.redirectToMyPage = function () {
                window.location.href = App.makeUrl("/Photomodel/Details?shortcut=" + this.model.get("shortcut"));
            };
            CreateView.prototype.checkRequired = function (e) {
                this.changeInput(e);
                if (this.model.has('firstname') && this.model.has('lastname') && this.model.has('shortcut')) {
                    this.$el.find('[data-role-next-step]').removeAttr("disabled");
                }
            };
            return CreateView;
        })(App.BaseViews.BaseCreateView);
        Views.CreateView = CreateView;
        var EditView = (function (_super) {
            __extends(EditView, _super);
            function EditView(model) {
                this.events = {
                    "change [data-role-input-optional]": "changeInput",
                    "change [data-role-input-required]": "changeInput",
                    "click [data-role-close-modal]": "closeModal",
                    "click [data-role-save]": "save",
                    "click [data-role-save-phone]": "savePhone",
                    "click [data-role-send-to-validate]": "sendToValidate",
                    "click [data-role-to-adminpanel]": "redirectToAdminPanel",
                    "click [data-role-phone-delete]": "deletePhone",
                };
                _super.call(this, model);
            }
            EditView.prototype.initialize = function (options) {
                var _this = this;
                var self = this;
                self.model = new App.PhotoModelModel({ id: options.routeId });
                this.listenTo(this.model, "sync", this.success);
                this.listenTo(this.model, "error", this.error);
                var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
                var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";
                var cropAvatar = new Croppic('role-avatar', {
                    customUploadButtonId: "role-change-avatar",
                    cropUrl: curl,
                    modal: true,
                    uploadUrl: url,
                    width: 400,
                    height: 400,
                    onAfterImgCrop: function (data) {
                        self.model.set("TeaserImage", data.url);
                    }
                });
                $.each($(".ionRangeSlider"), function () {
                    var selector = _this;
                    var ionoptions = {
                        min: parseInt($(selector).attr('data-min')),
                        max: parseInt($(selector).attr('value')),
                        from: parseInt($(selector).attr('value')),
                        type: 'single',
                        step: 1,
                        postfix: 'см',
                        prettify_enabled: false,
                        hasGrid: false,
                        onChange: function (data) {
                            var $this = $(data.input);
                            var id = $this.attr("id");
                            self.model.set(self.toCamelCase(id), data.fromNumber);
                        }
                    };
                    $(selector).ionRangeSlider(ionoptions);
                });
            };
            EditView.prototype.render = function () {
                this.setElement($("body"));
                this.delegateEvents();
                return this;
            };
            return EditView;
        })(App.BaseViews.BaseEditView);
        Views.EditView = EditView;
        var ModelDetails = (function (_super) {
            __extends(ModelDetails, _super);
            function ModelDetails(model) {
                this.el = "body";
                this.events = {
                    "click .clicl": "click"
                };
                _super.call(this, model);
            }
            ModelDetails.prototype.initialize = function (options) { };
            ModelDetails.prototype.render = function () {
                var view = new App.Views.WallView();
                this.$("#wall-placeholder").html(view.render().el);
                // app.WallView = view;
                return this;
            };
            ModelDetails.prototype.click = function (event) { };
            return ModelDetails;
        })(Backbone.View);
        Views.ModelDetails = ModelDetails;
        var ModelView = (function (_super) {
            __extends(ModelView, _super);
            function ModelView(model) {
                this.template = App.Tools.getTemplate("#model-summary-template");
                this.events = {
                    "click .clicl": "click"
                };
                _super.call(this, model);
            }
            ModelView.prototype.initialize = function (options) { };
            ModelView.prototype.render = function () {
                this.setElement(this.template(this.model.toJSON()));
                return this;
            };
            ModelView.prototype.click = function (event) { };
            return ModelView;
        })(Backbone.View);
        Views.ModelView = ModelView;
        var ModelsView = (function (_super) {
            __extends(ModelsView, _super);
            function ModelsView(model) {
                this.events = {};
                _super.call(this, model);
            }
            ModelsView.prototype.initialize = function (options) {
                this.topModelsCollection = new App.Collections.ModelsCollection();
                this.modelsCollection = new App.Collections.ModelsCollection();
                this.listenTo(this.modelsCollection, "reset", this.addModels);
                this.listenTo(this.modelsCollection, "add", this.addModel);
                this.listenTo(this.topModelsCollection, "reset", this.addTopModels);
                this.listenTo(this.topModelsCollection, "add", this.addTopModel);
            };
            ModelsView.prototype.render = function () {
                return this;
            };
            ModelsView.prototype.addModels = function () {
                this.modelsCollection.each(this.addModel);
                return this;
            };
            ModelsView.prototype.addTopModels = function () {
                this.topModelsCollection.each(this.addTopModel);
                return this;
            };
            ModelsView.prototype.addTopModel = function (model) {
                var view = new ModelView({ model: model });
                $("#top-models").append(view.render().el);
            };
            ModelsView.prototype.addModel = function (model) {
                var view = new ModelView({ model: model });
                $("#models").append(view.render().el);
            };
            return ModelsView;
        })(Backbone.View);
        Views.ModelsView = ModelsView;
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
///<reference path="../../SharedViews/BaseViews/AbstractView.ts"/>
/// <reference path="../../../typings/croppic/Croppic.d.ts"/>
///<reference path="../../Models/Routes/PhotorentModel.ts"/>
var App;
(function (App) {
    var Photorent;
    (function (Photorent) {
        var PhotorentModel = App.Models.PhotorentModel;
        var CreateView = (function (_super) {
            __extends(CreateView, _super);
            function CreateView(model) {
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
                _super.call(this, model);
            }
            CreateView.prototype.initialize = function (options) {
                var model = this.model = new PhotorentModel();
                this.listenTo(model, "sync", this.success);
                this.listenTo(model, "error", this.error);
                var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
                var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";
                var cropAvatar = new Croppic('role-avatar', {
                    customUploadButtonId: "role-change-avatar",
                    cropUrl: curl,
                    modal: true,
                    width: 400,
                    height: 400,
                    uploadUrl: url,
                    onAfterImgCrop: function (data) {
                        model.set("TeaserImage", data.url);
                    }
                });
                var cropCover = new Croppic('role-cover', {
                    customUploadButtonId: "role-change-cover",
                    cropUrl: curl,
                    uploadUrl: url,
                    onAfterImgCrop: function (data) {
                        model.set("CoverImage", data.url);
                    },
                    imgEyecandy: true,
                    imgEyecandyOpacity: 0.2,
                });
                var progressbar = $("#progressbar");
                var bar = progressbar.find('.uk-progress-bar');
                var settings = {
                    action: $('#config-settings').attr('data-upload-url') + "/Api/Files",
                    single: false,
                    allow: '*.(jpg|jpeg|gif|png)',
                    loadstart: function () {
                        bar.css("width", "0%").text("0%");
                        progressbar.removeClass("uk-hidden");
                    },
                    progress: function (percent) {
                        percent = Math.ceil(percent);
                        bar.css("width", percent + "%").text(percent + "%");
                    },
                    allcomplete: function (response) {
                        $.ajax({
                            url: App.makeUrl("/vjson/RoutePhotos/post"),
                            method: "POST",
                            data: { id: model.get("id"), photos: eval(response) }
                        }).done(function () {
                            $.each(eval(response), function () {
                                $(' <li><div class="uk-thumbnail"><img src="' + this.url + '" alt=""><div class="uk-thumbnail-caption uk-text-center">Главное фото</div></div></li>').appendTo('[data-role-picture-placeholder]');
                            });
                            $(this).addClass("done");
                        });
                    }
                };
                var select = new UIkit.uploadSelect($("#upload-select"), settings);
                var drop = new UIkit.uploadDrop($("#upload-drop"), settings);
                this.$el.find("[data-role-menu]").find("li").not('.uk-active').addClass("uk-disabled");
            };
            CreateView.prototype.render = function () {
                this.setElement($("body"));
                this.delegateEvents();
                return this;
            };
            CreateView.prototype.redirectToEdit = function () {
                window.location.href = App.makeUrl("/Admin/Photorent/Edit?shortcut=" + this.model.get("shortcut"));
            };
            CreateView.prototype.redirectToMyPage = function () {
                window.location.href = App.makeUrl("/Photorent/Details?shortcut=" + this.model.get("shortcut"));
            };
            CreateView.prototype.checkRequired = function (e) {
                this.changeInput(e);
                if (this.model.has('name') && this.model.has('shortcut')) {
                    this.$el.find('[data-role-next-step]').removeAttr("disabled");
                }
            };
            return CreateView;
        })(App.BaseViews.BaseCreateView);
        Photorent.CreateView = CreateView;
        var EditView = (function (_super) {
            __extends(EditView, _super);
            function EditView(model) {
                this.events = {
                    "change [data-role-input-optional]": "changeInput",
                    "change [data-role-input-required]": "changeInput",
                    "click [data-role-close-modal]": "closeModal",
                    "click [data-role-save]": "next",
                    "click [data-role-save-phone]": "savePhone",
                    "click [data-role-send-to-validate]": "sendToValidate",
                    "click [data-role-to-adminpanel]": "redirectToAdminPanel",
                    "click [data-role-phone-delete]": "deletePhone"
                };
                _super.call(this, model);
            }
            EditView.prototype.initialize = function (options) {
                var self = this;
                self.model = new PhotorentModel({ id: options.routeId });
                this.listenTo(this.model, "sync", this.success);
                this.listenTo(this.model, "error", this.error);
                var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
                var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";
                var cropCover = new Croppic('role-cover', {
                    customUploadButtonId: "role-change-cover",
                    cropUrl: curl,
                    uploadUrl: url,
                    onAfterImgCrop: function (data) {
                        self.model.set("CoverImage", data.url);
                    },
                    imgEyecandy: true,
                    imgEyecandyOpacity: 0.2,
                    cropData: {
                        "zoomFactor": 2,
                    }
                });
                var cropAvatar = new Croppic('role-avatar', {
                    customUploadButtonId: "role-change-avatar",
                    cropUrl: curl,
                    modal: true,
                    uploadUrl: url,
                    width: 400,
                    height: 400,
                    onAfterImgCrop: function (data) {
                        self.model.set("TeaserImage", data.url);
                    }
                });
            };
            EditView.prototype.render = function () {
                this.setElement($("body"));
                this.delegateEvents();
                return this;
            };
            return EditView;
        })(App.BaseViews.BaseEditView);
        Photorent.EditView = EditView;
        var PhotorentDetails = (function (_super) {
            __extends(PhotorentDetails, _super);
            function PhotorentDetails(model) {
                this.el = "body";
                this.technics = new App.Collections.PhototechnicsCollection();
                this.searchCollection = new App.Collections.PhototechnicsCollection();
                this.requestData = new App.Models.RequestData();
                this.bookingTemplate = App.Tools.getTemplate("#booking-view-template");
                this.events = {
                    "click .role-hall-tabs button": "changeCalendar",
                    "click .booking-button": "booking",
                    "click .role-clean-data": "cleanData",
                    "click .role-booking-toturial": "showBookingToturial",
                    "click .role-show-calendar": "showModalCalendar",
                    "click .role-category": "sortCategory",
                    "click .role-filter": "sortFilter",
                    "click .role-paginator-placeholder a": "sortPage",
                    "click [data-role-brand-select]": "sortBrand",
                    "keyup [data-role-search-input]": "search",
                    "click [data-role-search-input]": "showSearch"
                };
                _super.call(this, model);
            }
            PhotorentDetails.prototype.initialize = function (options) {
                var self = this;
                this.searchCollection.url = App.makeUrl("/api/search");
                this.requestData = options.requestData || {};
                this.requestData.routeId = App.routeId;
                this.listenTo(this.technics, 'reset', this.resetTechnics);
                this.listenTo(this.technics, "add", this.addTechnic);
                this.listenTo(this.searchCollection, "reset", this.resetSearch);
                this.listenTo(this.searchCollection, "add", this.addSearchTechnic);
            };
            PhotorentDetails.prototype.render = function () {
                var view = new App.Views.WallView();
                this.technics.reset(this.model.technics.elements);
                this.$("#wall-placeholder").html(view.render().el);
                App.WallView = view;
                return this;
            };
            PhotorentDetails.prototype.selectCategory = function (event) {
                var _this = this;
                var categoryID = $(event.target).attr('data-categoryid');
                $.get(App.makeUrl("/api/rentcalendar/"), { categoryID: categoryID }, function (data) { return _this.technics.reset(data.elements); });
            };
            PhotorentDetails.prototype.fetchSuccess = function (data) {
                $('.role-paginator-placeholder').html('');
                for (var i = 0; i < data.pagesCount; i++) {
                    $('.role-paginator-placeholder').append('<li><a href="#page-1" data-page="' + i + '">' + i + '</a></li>');
                }
                this.technics.reset(data.elements);
            };
            PhotorentDetails.prototype.sortCategory = function (event) {
                var _this = this;
                this.requestData.brandid = null;
                this.requestData.page = 0;
                this.requestData.categoryid = $(event.target).attr("data-categoryid");
                $.get(App.makeUrl("/api/RentPosition"), this.requestData, function (data) { _this.fetchSuccess(data); });
            };
            PhotorentDetails.prototype.sortBrand = function (event) {
                var _this = this;
                this.requestData.page = 0;
                this.requestData.brandid = $(event.target).attr("data-brand-id");
                $.get(App.makeUrl("/api/RentPosition"), this.requestData, function (data) { _this.fetchSuccess(data); });
            };
            PhotorentDetails.prototype.sortFilter = function (event) {
                var _this = this;
                this.requestData.order = $(event.target).attr("data-val");
                $.get(App.makeUrl("/api/RentPosition"), this.requestData, function (data) { _this.fetchSuccess(data); });
            };
            PhotorentDetails.prototype.sortPage = function (event) {
                var _this = this;
                this.requestData.page = +$(event.target).attr("data-page");
                $.get(App.makeUrl("/api/RentPosition"), this.requestData, function (data) { _this.fetchSuccess(data); });
            };
            PhotorentDetails.prototype.addTechnic = function (item) {
                var placeholder = this.$('[data-role-PhototechnicsViewModel-placeholder]');
                (new App.Views.PhototechnicSummary({ model: item })).render().$el.appendTo(placeholder);
            };
            PhotorentDetails.prototype.resetTechnics = function () {
                this.$('[data-role-PhototechnicsViewModel-placeholder]').html("");
                this.technics.each(this.addTechnic);
                return this;
            };
            PhotorentDetails.prototype.closeAll = function () {
                this.$(".uk-open").removeClass("uk-open");
            };
            PhotorentDetails.prototype.showSearch = function (e) {
                var _this = this;
                $(e.currentTarget).parent().addClass("uk-open");
                $("body").one("click", function () { return _this.closeAll(); });
            };
            PhotorentDetails.prototype.search = function (e) {
                var self = this;
                var val = $(e.currentTarget).val();
                if (val.length < 3)
                    return;
                this.searchCollection.fetch({ data: { routeId: App.routeId, search: val }, reset: true });
            };
            PhotorentDetails.prototype.resetSearch = function () {
                var _this = this;
                this.$el.find('[data-role-search-results-placeholder]').html('');
                this.searchCollection.each(function (model) { return _this.addSearchTechnic(model); });
                return this;
            };
            PhotorentDetails.prototype.addSearchTechnic = function (model) {
                var view = new App.Views.PhototechnicSummary({ model: model, version: "search", className: "uk-width-1-1" });
                view.render();
                this.$el.find('[data-role-search-results-placeholder]').append(view.el);
            };
            return PhotorentDetails;
        })(Backbone.View);
        Photorent.PhotorentDetails = PhotorentDetails;
        var PhotorentView = (function (_super) {
            __extends(PhotorentView, _super);
            function PhotorentView(model) {
                this.events = {
                    "click #clicl": "click"
                };
                _super.call(this, model);
            }
            PhotorentView.prototype.initialize = function (options) { };
            PhotorentView.prototype.render = function () {
                return this;
            };
            PhotorentView.prototype.click = function (event) { };
            return PhotorentView;
        })(Backbone.View);
        Photorent.PhotorentView = PhotorentView;
    })(Photorent = App.Photorent || (App.Photorent = {}));
})(App || (App = {}));
///<reference path="../../SharedViews/BaseViews/AbstractView.ts"/>
/// <reference path="../../../typings/croppic/Croppic.d.ts"/>
///<reference path="../../Models/Routes/PhotoshopModel.ts"/>
var App;
(function (App) {
    var Models;
    (function (Models) {
        var RequestData = (function () {
            function RequestData() {
                this.page = 0;
            }
            return RequestData;
        })();
        Models.RequestData = RequestData;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Photoshop;
    (function (Photoshop_1) {
        var RequestData = App.Models.RequestData;
        var CreateView = (function (_super) {
            __extends(CreateView, _super);
            function CreateView(model) {
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
                _super.call(this, model);
            }
            CreateView.prototype.initialize = function (options) {
                var self = this;
                var model = this.model = new App.PhotoshopModel();
                this.listenTo(model, "sync", this.success);
                this.listenTo(model, "error", this.error);
                var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
                var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";
                var cropAvatar = new Croppic('role-avatar', {
                    customUploadButtonId: "role-change-avatar",
                    cropUrl: curl,
                    modal: true,
                    width: 400,
                    height: 400,
                    uploadUrl: url,
                    onAfterImgCrop: function (data) {
                        model.set("TeaserImage", data.url);
                    }
                });
                var cropCover = new Croppic('role-cover', {
                    customUploadButtonId: "role-change-cover",
                    cropUrl: curl,
                    uploadUrl: url,
                    onAfterImgCrop: function (data) {
                        model.set("CoverImage", data.url);
                    },
                    imgEyecandy: true,
                    imgEyecandyOpacity: 0.2,
                });
                var progressbar = $("#progressbar");
                var bar = progressbar.find('.uk-progress-bar');
                var settings = {
                    action: $('#config-settings').attr('data-upload-url') + "/Api/Files",
                    single: false,
                    allow: '*.(jpg|jpeg|gif|png)',
                    loadstart: function () {
                        bar.css("width", "0%").text("0%");
                        progressbar.removeClass("uk-hidden");
                    },
                    progress: function (percent) {
                        percent = Math.ceil(percent);
                        bar.css("width", percent + "%").text(percent + "%");
                    },
                    allcomplete: function (response) {
                        $.ajax({
                            url: "/vjson/RoutePhotos/post",
                            method: "POST",
                            data: { id: model.get("id"), photos: eval(response) }
                        }).done(function () {
                            $.each(eval(response), function () {
                                $(' <li><div class="uk-thumbnail"><img src="' + this.url + '" alt=""><div class="uk-thumbnail-caption uk-text-center">Главное фото</div></div></li>').appendTo('[data-role-picture-placeholder]');
                            });
                            $(this).addClass("done");
                        });
                    }
                };
                var select = new UIkit.uploadSelect($("#upload-select"), settings);
                var drop = new UIkit.uploadDrop($("#upload-drop"), settings);
                ymaps.ready(function () {
                    self.myMap = new ymaps.Map("map", {
                        center: [55.76, 37.64],
                        zoom: 9
                    });
                    self.myMap.events.add('click', function (e) {
                        // To get the geographical coordinates of the point of click,
                        // call .get('coordPosition')
                        var position = e.get('coords');
                        if (typeof (self.placemark) != 'undefined')
                            self.myMap.geoObjects.remove(self.placemark);
                        self.placemark = new ymaps.Placemark(position);
                        self.model.set("latitude", position[0]);
                        self.model.set("longitude", position[1]);
                        self.myMap.geoObjects.add(self.placemark);
                    });
                });
                this.$el.find("[data-role-menu]").find("li").not('.uk-active').addClass("uk-disabled");
            };
            CreateView.prototype.render = function () {
                this.setElement($("body"));
                this.delegateEvents();
                return this;
            };
            CreateView.prototype.redirectToEdit = function () {
                window.location.href = "/Admin/Photoshop/Edit?shortcut=" + this.model.get("shortcut");
            };
            CreateView.prototype.redirectToMyPage = function () {
                window.location.href = "/Photoshop/Details?shortcut=" + this.model.get("shortcut");
            };
            CreateView.prototype.checkRequired = function (e) {
                this.changeInput(e);
                if (this.model.has('name') && this.model.has('shortcut')) {
                    this.$el.find('button.uk-button-next-step').removeAttr("disabled");
                }
            };
            return CreateView;
        })(App.BaseViews.BaseCreateView);
        Photoshop_1.CreateView = CreateView;
        var EditView = (function (_super) {
            __extends(EditView, _super);
            function EditView(model) {
                this.events = {
                    "change [data-role-input-optional]": "changeInput",
                    "change [data-role-input-required]": "changeInput",
                    "click [data-role-close-modal]": "closeModal",
                    "click [data-role-save]": "save",
                    "click [data-role-save-phone]": "savePhone",
                    "click [data-role-send-to-validate]": "sendToValidate",
                    "click [data-role-to-adminpanel]": "redirectToAdminPanel",
                    "click [data-role-phone-delete]": "deletePhone",
                };
                _super.call(this, model);
            }
            EditView.prototype.initialize = function (options) {
                var self = this;
                self.model = new App.PhotoshopModel({ id: options.routeId });
                this.listenTo(this.model, "sync", this.success);
                this.listenTo(this.model, "error", this.error);
                var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
                var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";
                var cropCover = new Croppic('role-cover', {
                    customUploadButtonId: "role-change-cover",
                    cropUrl: curl,
                    uploadUrl: url,
                    onAfterImgCrop: function (data) {
                        self.model.set("CoverImage", data.url);
                    },
                    imgEyecandy: true,
                    imgEyecandyOpacity: 0.2,
                    cropData: {
                        "zoomFactor": 2,
                    }
                });
                var cropAvatar = new Croppic('role-avatar', {
                    customUploadButtonId: "role-change-avatar",
                    cropUrl: curl,
                    modal: true,
                    uploadUrl: url,
                    width: 400,
                    height: 400,
                    onAfterImgCrop: function (data) {
                        self.model.set("TeaserImage", data.url);
                    }
                });
            };
            EditView.prototype.render = function () {
                this.setElement($("body"));
                this.delegateEvents();
                return this;
            };
            return EditView;
        })(App.BaseViews.BaseEditView);
        Photoshop_1.EditView = EditView;
        var Photoshop = (function (_super) {
            __extends(Photoshop, _super);
            function Photoshop(model) {
                this.el = "body";
                this.vitrineCollection = new App.Collections.PhototechnicsCollection();
                this.searchCollection = new App.Collections.PhototechnicsCollection();
                this.requestData = new RequestData();
                this.events = {
                    "click .role-booking-button": "booking",
                    "click .role-clean-data": "cleanData",
                    "click .role-booking-toturial": "showBookingToturial",
                    "click .role-category": "sortCategory",
                    "click .role-filter": "sortFilter",
                    "click .role-paginator-placeholder a": "sortPage",
                    "click [data-role-brand-select]": "sortBrand",
                    "keyup [data-role-search-input]": "search",
                    "click [data-role-search-input]": "showSearch",
                    "click [data-role-send-request]": "sendRequest"
                };
                _super.call(this, model);
            }
            Photoshop.prototype.initialize = function (options) {
                var self = this;
                this.searchCollection.url = App.makeUrl("/api/search");
                this.requestData = options.requestData || {};
                this.requestData.routeId = App.routeId;
                this.listenTo(this.vitrineCollection, "reset", this.resetVitrine);
                this.listenTo(this.vitrineCollection, "add", this.addVitrineTechnic);
                this.listenTo(this.searchCollection, "reset", this.resetSearch);
                this.listenTo(this.searchCollection, "add", this.addSearchTechnic);
                //this.vitrineCollection.reset(this.model.technics.elements);
                this.shoppingCart = this.shoppingCart || App.Tools.cookieList("shoppingCart");
                $("[data-role-items-count]").html(this.shoppingCart.items().length);
            };
            Photoshop.prototype.render = function () {
                var view = new App.Views.WallView();
                this.$("#wall-placeholder").html(view.render().el);
                App.WallView = view;
                return this;
            };
            Photoshop.prototype.closeAll = function () {
                this.$(".uk-open").removeClass("uk-open");
            };
            Photoshop.prototype.resetVitrine = function () {
                var self = this;
                this.$el.find('.role-vitrine').html('');
                this.$el.find('.role-horizontal-vitrine').html('');
                this.vitrineCollection.each(function (model) {
                    return self.addVitrineTechnic(model);
                });
                return this;
            };
            Photoshop.prototype.addVitrineTechnic = function (model) {
                var view = new App.Views.PhototechnicSummary({ model: model });
                view.render();
                this.$el.find('.role-vitrine').append(view.el);
                view = new App.Views.PhototechnicSummary({ model: model, version: 'horizontal', className: "uk-width-1-1" });
                view.render();
                this.$el.find('.role-horizontal-vitrine').append(view.el);
            };
            Photoshop.prototype.sortCategory = function (event) {
                var self = this;
                this.requestData.brandid = null;
                this.requestData.page = 0;
                this.requestData.categoryid = $(event.target).attr("data-categoryid");
                $.get(App.makeUrl("/api/PricePosition"), this.requestData, function (data) { self.fetchSuccess(data); });
            };
            Photoshop.prototype.sortBrand = function (event) {
                var self = this;
                this.requestData.page = 0;
                this.requestData.brandid = $(event.target).attr("data-brand-id");
                $.get(App.makeUrl("/api/PricePosition"), this.requestData, function (data) { self.fetchSuccess(data); });
            };
            Photoshop.prototype.sortFilter = function (event) {
                var self = this;
                this.requestData.order = $(event.target).attr("data-val");
                $.get(App.makeUrl("/api/PricePosition"), this.requestData, function (data) { self.fetchSuccess(data); });
            };
            Photoshop.prototype.sortPage = function (event) {
                var self = this;
                this.requestData.page = +$(event.target).attr("data-page");
                $.get(App.makeUrl("/api/PricePosition"), this.requestData, function (data) { self.fetchSuccess(data); });
            };
            Photoshop.prototype.fetchSuccess = function (data) {
                $('.role-paginator-placeholder').html('');
                for (var i = 0; i < data.pagesCount; i++) {
                    $('.role-paginator-placeholder').append('<li><a href="#page-1" data-page="' + i + '">' + i + '</a></li>');
                }
                this.vitrineCollection.reset(data.elements);
            };
            Photoshop.prototype.booking = function () {
                var self = this;
            };
            Photoshop.prototype.successSync = function (model, resp, options) {
            };
            Photoshop.prototype.errorSync = function (model, xhr, options) {
                UIkit.notify({
                    message: 'Невозможно забронировать студию с такими параметрами, проверьте введенную информацию',
                    status: 'danger',
                    timeout: 5000,
                    pos: 'bottom-right'
                });
            };
            Photoshop.prototype.showSearch = function (e) {
                var _this = this;
                $(e.currentTarget).parent().addClass("uk-open");
                $("body").one("click", function () {
                    _this.closeAll();
                });
            };
            Photoshop.prototype.search = function (e) {
                var self = this;
                var val = $(e.currentTarget).val();
                if (val.length < 3)
                    return;
                this.searchCollection.fetch({ data: { routeid: App.routeId, search: val }, reset: true });
            };
            Photoshop.prototype.resetSearch = function () {
                var self = this;
                this.$el.find('[data-role-search-results-placeholder]').html('');
                this.searchCollection.each(function (model) {
                    return self.addSearchTechnic(model);
                });
                return this;
            };
            Photoshop.prototype.addSearchTechnic = function (model) {
                var view = new App.Views.PhototechnicSummary({ model: model, version: 'search', className: "uk-width-1-1" });
                view.render();
                this.$el.find('[data-role-search-results-placeholder]').append(view.el);
            };
            Photoshop.prototype.sendRequest = function () {
                //var client = new $.RestClient(App.makeUrl("/api/"));
                //client.add("callback");
                //client.callback.create({ routeID: App.routeId, phone: $("[data-role-pohone-number]").val() });
                //var modal = new UIkit.modal("[data-role-dialog]");
                //modal.hide();
            };
            return Photoshop;
        })(Backbone.View);
        Photoshop_1.Photoshop = Photoshop;
    })(Photoshop = App.Photoshop || (App.Photoshop = {}));
})(App || (App = {}));
///<reference path="../../SharedViews/BaseViews/AbstractView.ts"/>
/// <reference path="../../../typings/croppic/Croppic.d.ts"/>
///<reference path="../../Models/Routes/PhotostudioModel.ts"/>
///<reference path="../../../typings/tagit/tagit.d.ts"/>
var App;
(function (App) {
    var Views;
    (function (Views) {
        var Photostudio;
        (function (Photostudio) {
            var PhotomskCalendarOptions = App.Views.PhotomskCalendarOptions;
            var CreateView = (function (_super) {
                __extends(CreateView, _super);
                function CreateView(model) {
                    this.events = {
                        "change input": "changeInput",
                        //"change [data-role-input-optional]": "changeInput",
                        //"change [data-role-input-required]": "changeInput",
                        "click [data-role-close-modal]": "closeModal",
                        "click [data-role-next-step]": "save",
                        "click [data-role-prev-step]": "prev",
                        "click [data-role-send-to-validate]": "sendToValidate",
                        "click [data-role-save-phone]": "savePhone",
                        "click [data-role-to-adminpanel]": "redirectToAdminPanel",
                        "click [data-role-to-edit]": "redirectToEdit",
                        "click [data-role-to-page]": "redirectToMyPage"
                    };
                    _super.call(this, model);
                }
                CreateView.prototype.initialize = function (options) {
                    var model = this.model = new App.PhotostudioModel();
                    var self = this;
                    this.listenTo(this.model, "sync", this.success);
                    this.listenTo(this.model, "error", this.error);
                    var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
                    var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";
                    var cropCover = new Croppic('role-cover', {
                        customUploadButtonId: "role-change-cover",
                        cropUrl: curl,
                        uploadUrl: url,
                        onAfterImgCrop: function (data) {
                            model.set("CoverImage", data.url);
                        },
                        imgEyecandy: true,
                        imgEyecandyOpacity: 0.2,
                        cropData: {
                            "zoomFactor": 2,
                        }
                    });
                    var cropAvatar = new Croppic('role-avatar', {
                        customUploadButtonId: "role-change-avatar",
                        cropUrl: curl,
                        modal: true,
                        uploadUrl: url,
                        width: 400,
                        height: 400,
                        onAfterImgCrop: function (data) {
                            model.set("TeaserImage", data.url);
                        }
                    });
                    var progressbar = $("#progressbar");
                    var bar = progressbar.find('.uk-progress-bar');
                    var settings = {
                        action: $('#config-settings').attr('data-upload-url') + "/Api/Files",
                        single: false,
                        allow: '*.(jpg|jpeg|gif|png)',
                        loadstart: function () {
                            bar.css("width", "0%").text("0%");
                            progressbar.removeClass("uk-hidden");
                        },
                        progress: function (percent) {
                            percent = Math.ceil(percent);
                            bar.css("width", percent + "%").text(percent + "%");
                        },
                        allcomplete: function (response) {
                            $.ajax({
                                url: App.makeUrl("/vjson/RoutePhotos/post"),
                                method: "POST",
                                data: { id: model.get("id"), photos: eval(response) }
                            }).done(function () {
                                $.each(eval(response), function () {
                                    $(' <li><div class="uk-thumbnail"><img src="' + this.url + '" alt=""><div class="uk-thumbnail-caption uk-text-center">Главное фото</div></div></li>').appendTo('[data-role-picture-placeholder]');
                                });
                                $(this).addClass("done");
                            });
                        }
                    };
                    var select = new UIkit.uploadSelect($("#upload-select"), settings);
                    var drop = new UIkit.uploadDrop($("#upload-drop"), settings);
                    ymaps.ready(function () {
                        self.myMap = new ymaps.Map("map", {
                            center: [55.76, 37.64],
                            zoom: 9
                        });
                        self.myMap.events.add('click', function (e) {
                            // To get the geographical coordinates of the point of click,
                            // call .get('coordPosition')
                            var position = e.get('coords');
                            if (typeof (self.placemark) != 'undefined')
                                self.myMap.geoObjects.remove(self.placemark);
                            self.placemark = new ymaps.Placemark(position);
                            self.model.set("latitude", position[0]);
                            self.model.set("longitude", position[1]);
                            self.myMap.geoObjects.add(self.placemark);
                        });
                    });
                    this.$el.find("[data-role-menu]").find("li").not('.uk-active').addClass("uk-disabled");
                };
                CreateView.prototype.render = function () {
                    this.setElement($("body"));
                    this.delegateEvents();
                    return this;
                };
                CreateView.prototype.redirectToEdit = function () {
                    window.location.href = "/Admin/Photostudio/Edit?shortcut=" + this.model.get("shortcut");
                };
                CreateView.prototype.redirectToMyPage = function () {
                    window.location.href = "/Photostudio/Details?shortcut=" + this.model.get("shortcut");
                };
                CreateView.prototype.checkRequired = function (e) {
                    this.changeInput(e);
                    if (this.model.has("name") && this.model.has("shortcut")) {
                        this.$el.find('button.uk-button-next-step').removeAttr("disabled");
                    }
                };
                return CreateView;
            })(App.BaseViews.BaseCreateView);
            Photostudio.CreateView = CreateView;
            var EditView = (function (_super) {
                __extends(EditView, _super);
                function EditView(model) {
                    this.events = {
                        "change input": "changeInput",
                        //"change [data-role-input-optional]": "changeInput",
                        //"change [data-role-input-required]": "changeInput",
                        "click [data-role-close-modal]": "closeModal",
                        "click [data-role-save]": "save",
                        "click [data-role-save-phone]": "savePhone",
                        "click [data-role-send-to-validate]": "sendToValidate",
                        "click [data-role-to-adminpanel]": "redirectToAdminPanel",
                        "click [data-role-phone-delete]": "deletePhone"
                    };
                    _super.call(this, model);
                }
                EditView.prototype.initialize = function (options) {
                    var self = this;
                    self.model = new App.PhotostudioModel({ id: options.routeId });
                    this.listenTo(this.model, "sync", this.success);
                    this.listenTo(this.model, "error", this.error);
                    var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
                    var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";
                    var cropCover = new Croppic('role-cover', {
                        customUploadButtonId: "role-change-cover",
                        cropUrl: curl,
                        uploadUrl: url,
                        onAfterImgCrop: function (data) {
                            self.model.set("CoverImage", data.url);
                        },
                        imgEyecandy: true,
                        imgEyecandyOpacity: 0.2,
                        cropData: {
                            "zoomFactor": 2,
                        }
                    });
                    var cropAvatar = new Croppic('role-avatar', {
                        customUploadButtonId: "role-change-avatar",
                        cropUrl: curl,
                        modal: true,
                        uploadUrl: url,
                        width: 400,
                        height: 400,
                        onAfterImgCrop: function (data) {
                            self.model.set("TeaserImage", data.url);
                        }
                    });
                };
                EditView.prototype.render = function () {
                    this.setElement($("body"));
                    this.delegateEvents();
                    return this;
                };
                EditView.prototype.deletePhoto = function (evt) {
                    var id = $(evt.target).attr("data-id");
                    $.ajax({
                        url: App.makeUrl('/api/RoutePhotos/' + id),
                        type: 'DELETE',
                        success: function (result) {
                            // Do something with the result
                        }
                    });
                };
                return EditView;
            })(App.BaseViews.BaseEditView);
            Photostudio.EditView = EditView;
            var StudioDetails = (function (_super) {
                __extends(StudioDetails, _super);
                function StudioDetails(model) {
                    this.el = "body";
                    this.events = {
                        "click .role-booking-button": "booking",
                        "click .role-clean-data": "cleanData",
                        "click .role-booking-toturial": "showBookingToturial"
                    };
                    this.bookingTemplate = App.Tools.getTemplate("#booking-view-template");
                    _super.call(this, model);
                }
                StudioDetails.prototype.initialize = function (options) {
                    this.eventCollection = new App.Collections.EventCollection();
                    this.validateEventCollection = new App.Collections.EventCollection();
                    this.listenTo(this.validateEventCollection, "add", this.addValidatedEvent);
                    this.requestID = App.Tools.guid();
                };
                StudioDetails.prototype.render = function () {
                    this.wallView = new Views.WallView();
                    this.calendar = new Views.PhotomskCalendar({
                        model: this.model,
                        calendarOption: { slotDuration: "00:30", snapDuration: "01:00", eventClick: function (calEvent, jsEvent, view) { } }
                    });
                    $('.role-booking-calendar').html(this.calendar.render().el);
                    this.calendar.updateView();
                    this.listenTo(this.calendar, 'eventAdded', this.eventAdded);
                    this.$("[data-role-wall-placeholder]").html(this.wallView.render().el);
                    return this;
                };
                StudioDetails.prototype.eventAdded = function (event) {
                    var self = this;
                    $.get(App.makeUrl("/api/Events"), { validate: true, start: event.start.format(), end: event.end.format(), calendarIDs: [event.calendarID] }, function (data) {
                        if (data.error) {
                            UIkit.notify({
                                message: data.status,
                                status: 'danger',
                                timeout: 5000,
                                pos: 'bottom-right'
                            });
                            return;
                        }
                        $.each(data, function () {
                            self.validateEventCollection.add(this);
                        });
                    });
                };
                StudioDetails.prototype.removeEvent = function (event) { };
                StudioDetails.prototype.addValidatedEvent = function (event) {
                    this.$el.find("#booking").append(this.bookingTemplate(event.toJSON()));
                    var price = +0;
                    this.validateEventCollection.each(function (eevent) {
                        price += eevent.get('summ');
                    });
                    this.$('.role-booking-total-price').html(App.Tools.formatMoney(price));
                };
                StudioDetails.prototype.booking = function () {
                    var self = this;
                    if (this.validateEventCollection.length == 0) {
                        UIkit.notify({
                            message: 'Не выбрано время, бронируемое за человеком =)',
                            status: 'danger',
                            timeout: 5000,
                            pos: 'bottom-right'
                        });
                        return;
                    }
                    var model = new App.Models.BookingModel({ UserInformation: self.selectedUser, events: self.validateEventCollection.toJSON(), requestID: self.requestID });
                    this.listenTo(model, "sync", this.successSync);
                    this.listenTo(model, "error", this.errorSync);
                    model.save();
                };
                StudioDetails.prototype.successSync = function (model, resp, options) {
                    UIkit.notify({
                        message: 'студия успешно забронирована',
                        status: 'info',
                        timeout: 5000,
                        pos: 'bottom-right'
                    });
                    this.validateEventCollection.reset();
                    this.$el.find("#booking-placeholder").html('');
                };
                StudioDetails.prototype.errorSync = function (model, xhr, options) {
                    UIkit.notify({
                        message: 'Невозможно забронировать студию с такими параметрами, проверьте введенную информацию',
                        status: 'danger',
                        timeout: 5000,
                        pos: 'bottom-right'
                    });
                };
                StudioDetails.prototype.cleanData = function () {
                    this.validateEventCollection.reset();
                    this.eventCollection.reset();
                    this.$el.find("#booking-placeholder").html('');
                    //this.calendar.fullCalendar('removeEvents', function (event) {
                    //    return event.className == "role-add-booking";
                    //});
                };
                StudioDetails.prototype.showBookingToturial = function () {
                    (new App.Views.ToturialModal({ src: "//www.youtube.com/embed/Xog1_IqR5Jo" })).render().$el.appendTo("body");
                };
                return StudioDetails;
            })(Backbone.View);
            Photostudio.StudioDetails = StudioDetails;
            var StudioBooking = (function (_super) {
                __extends(StudioBooking, _super);
                function StudioBooking(model) {
                    this.events = {
                        "focus .role-user-information-search": "autofocus",
                        "blur .role-user-information-search": "autoblur",
                        "click .booking-button": "booking",
                        "click .role-hall-tabs button": "changeCalendar",
                        "click .role-aditional-show": "showAditional"
                    };
                    this.el = "body";
                    this.autoItem = App.Tools.getTemplate("#autocomplete-userinfo-view-template");
                    this.newitem = App.Tools.getTemplate("#autocomplete-new-view-template");
                    this.bookingTemplate = App.Tools.getTemplate("#booking-view-template");
                    _super.call(this, model);
                }
                StudioBooking.prototype.initialize = function (options) {
                    this.eventCollection = new App.Collections.EventCollection();
                    this.validateEventCollection = new App.Collections.EventCollection();
                    // this.listenTo(this.eventCollection, "add", this.addEvent);
                    this.listenTo(this.validateEventCollection, "add", this.addValidatedEvent);
                    var msko = new PhotomskCalendarOptions();
                    msko.model = this.model;
                    this.calendar = new App.Views.PhotomskCalendar(msko);
                };
                StudioBooking.prototype.render = function () {
                    var self = this;
                    var auto = this.$el.find(".role-user-information-search").autocomplete({
                        appendTo: this.$el.find(".uk-dropdown-search"),
                        source: function (request, respond) {
                            $.post("/vjson/Search/FindByPhone", { search: request.term }, function (response) {
                                if (response.length == 0) {
                                    this.addNewUserInfo();
                                }
                                respond(response);
                            });
                        },
                        minLength: 2,
                        select: function (event, ui) {
                            self.selectItem(ui.item);
                        }
                    });
                    auto.data("ui-autocomplete")._renderMenu = function (ul, item) {
                        return self.renderMenu(ul, item, this);
                    };
                    auto.data("ui-autocomplete")._renderItem = function (ul, item) {
                        return self.renderAutocompleteItem(ul, item);
                    };
                    $(".ui-autocomplete").attr("class", "uk-nav uk-nav-autocomplete uk-autocomplete-results");
                    $('.role-booking-calendar').html(this.calendar.render().el);
                    this.calendar.updateView();
                    this.listenTo(this.calendar, 'eventAdded', this.eventAdded);
                    //  $(".role-aditional-tags").tagit({});
                    return this;
                };
                StudioBooking.prototype.renderAutocompleteItem = function (ul, item) {
                    return $(this.autoItem(item)).appendTo(ul);
                };
                StudioBooking.prototype.renderMenu = function (ul, items, auto) {
                    var self = this;
                    $.each(items, function (index, item) {
                        auto._renderItemData(ul, item);
                    });
                    $(ul).append(self.newitem());
                };
                StudioBooking.prototype.autofocus = function () {
                    $(".uk-dropdown-search").show();
                };
                StudioBooking.prototype.autoblur = function () {
                    $(".uk-dropdown-search").hide();
                };
                StudioBooking.prototype.selectItem = function (item) {
                    this.newUser = false;
                    this.selectedUser = new App.Models.UserInformationModel({
                        userFirstName: item.firstName,
                        userlastName: item.lastName,
                        userPhone: item.userPhone,
                        admin: true
                    });
                    this.$el.find("#client-placeholder")
                        .html(new App.Views.UserInformation({ model: item }).render().el);
                };
                StudioBooking.prototype.eventAdded = function (event) {
                    this.eventCollection.add(event);
                    var self = this;
                    $.get(App.makeUrl("/api/Events"), { validate: false, start: event.start.format(), end: event.end.format(), calendarIDs: [event.calendarID] }, function (data) {
                        if (data.error) {
                            UIkit.notify({
                                message: data.status,
                                status: 'danger',
                                timeout: 5000,
                                pos: 'bottom-right'
                            });
                            return;
                        }
                        $.each(data, function () {
                            self.validateEventCollection.add(this);
                        });
                    });
                };
                StudioBooking.prototype.addValidatedEvent = function (event) {
                    this.$el.find("#booking").append(this.bookingTemplate(event.toJSON()));
                    var price = 0;
                    this.validateEventCollection.each(function (eevent) {
                        price += eevent.get('summ');
                    });
                    this.$('.role-booking-total-price').html(App.Tools.formatMoney(price));
                };
                StudioBooking.prototype.booking = function () {
                    var self = this;
                    if (this.newUser) {
                        this.selectedUser = new App.Models.UserInformationModel({
                            firstName: $("#firstName").val(),
                            lastName: $("#lastName").val(),
                            userPhone: $(".role-user-information-search").val(),
                            admin: true
                        });
                    }
                    if (typeof self.selectedUser == 'undefined' || self.selectedUser == null) {
                        UIkit.notify({
                            message: 'Не введена инфорамция о пользователе',
                            status: 'danger',
                            timeout: 5000,
                            pos: 'top-center'
                        });
                        return;
                    }
                    if (this.eventCollection.length == 0) {
                        UIkit.notify({
                            message: 'Не выбрано время, бронируемое за человеком =)',
                            status: 'danger',
                            timeout: 5000,
                            pos: 'top-center'
                        });
                        return;
                    }
                    var model = new App.Models.BookingModel({ isAdmin: true, UserInformation: self.selectedUser, events: self.eventCollection.toJSON() });
                    this.listenTo(model, "sync", this.successSync);
                    this.listenTo(model, "error", this.errorSync);
                    model.save();
                };
                StudioBooking.prototype.addNewUserInfo = function () {
                    this.newUser = true;
                    this.$el.find("#client-placeholder")
                        .html($("#user-information-edit-template").html());
                };
                StudioBooking.prototype.showAditional = function (event) {
                    $('.role-aditional-form').show();
                };
                StudioBooking.prototype.successSync = function () {
                    var self = this;
                    UIkit.notify({
                        message: 'Забронированно',
                        status: 'success',
                        timeout: 5000,
                        pos: 'top-center'
                    });
                    self.$("#client-placeholder").html('');
                    self.$('.role-user-information-search').val('+375');
                    self.$('.role-booking-total-price').html('0');
                    self.$('#booking').html('');
                    self.validateEventCollection.reset();
                    self.eventCollection.reset();
                    self.newUser = false;
                    self.calendar.reset();
                };
                StudioBooking.prototype.errorSync = function () {
                    UIkit.notify({
                        message: 'Невозможно забронировать студию с такими параметрами, проверьте введенную информацию',
                        status: 'danger',
                        timeout: 5000,
                        pos: 'top-center'
                    });
                };
                return StudioBooking;
            })(Backbone.View);
            Photostudio.StudioBooking = StudioBooking;
        })(Photostudio = Views.Photostudio || (Views.Photostudio = {}));
    })(Views = App.Views || (App.Views = {}));
})(App || (App = {}));
//# sourceMappingURL=App.js.map