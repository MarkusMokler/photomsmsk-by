(function ($) {
    /**
     *  Namespace: the namespace the plugin is located under
     *  pluginName: the name of the plugin
     */
    var extensionMethods = {
        /*
         * retrieve the id of the element
         * this is some context within the existing plugin
         */
        htmlEscape: function (s) {
            return "hello world";
        }
    };

    $.extend(true, $.fn.fullCalendar.prototype, extensionMethods);


})(jQuery);

(function($, undefined) {

    function htmlEscape(s) {
        return "hello world";
};})(jQuery);