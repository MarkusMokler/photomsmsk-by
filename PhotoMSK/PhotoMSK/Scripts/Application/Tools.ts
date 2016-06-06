module App.Tools {

    export declare var templates: {};

    export function getTemplate(templateString: string, settings?: _.TemplateSettings): (...data: any[]) => string {
        if (typeof Tools.templates == "undefined") {
            Tools.templates = {};
        }
        if (typeof Tools.templates[templateString] == "undefined") {
            Tools.templates[templateString] = _.template($(templateString).html(), settings);
        }
        return Tools.templates[templateString];
    };


    export function cookieList(cookieName: any) {
        //When the cookie is saved the items will be a comma seperated string
        //So we will split the cookie by comma to get the original array
        var cookie = $.cookie(cookieName);
        //Load the items or a new array if null.
        var items = cookie ? $.parseJSON(cookie) : new Array();

        //Return a object that we can use to access the array.
        //while hiding direct access to the declared items array
        //this is called closures see http://www.jibbering.com/faq/faq_notes/closures.html
        return {
            "add": val => {
                //Add to the items.
                items.push(val);
                //Save the items to a cookie.
                //EDIT: Modified from linked answer by Nick see 
                //      http://stackoverflow.com/questions/3387251/how-to-store-array-in-jquery-cookie
                $.cookie(cookieName, JSON.stringify(items));
            },
            "remove": val => {
                //EDIT: Thx to Assef and luke for remove.
                var indx = items.indexOf(val);
                if (indx != -1) items.splice(indx, 1);
                $.cookie(cookieName, items.join(','));
            },
            "clear": () => {
                items = null;
                //clear the cookie.
                $.cookie(cookieName, null);
            },
            "items": () => items
        }
    }

    export function formatMoney(n: number, decPlaces?, thouSeparator?, decSeparator?) {

        decPlaces = isNaN(decPlaces = Math.abs(decPlaces)) ? 0 : decPlaces;
        decSeparator = decSeparator == undefined ? "." : decSeparator;
        thouSeparator = thouSeparator == undefined ? " " : thouSeparator;
        var sign = n < 0 ? "-" : "";
        var m = "";
        var i = +parseInt(m = Math.abs(+n || 0).toFixed(decPlaces)) + "";

        var j = (j = i.length) > 3 ? j % 3 : 0;
        return sign + (j ? i.substr(0, j) + thouSeparator : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thouSeparator) + (decPlaces ? decSeparator + Math.abs(n - parseInt(i)).toFixed(decPlaces).slice(2) : "");
    };

    export function guid() {
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
                .toString(16)
                .substring(1);
        }
        return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
            s4() + '-' + s4() + s4() + s4();
    }

    export function htmlSubstring(s, n) {
        var m, r = /<([^>\s]*)[^>]*>/g,
            stack = [],
            lasti = 0,
            result = '';

        while ((m = r.exec(s)) && n) {
            var temp = s.substring(lasti, m.index).substr(0, n);
            result += temp;
            n -= temp.length;
            lasti = r.lastIndex;

            if (n) {
                result += m[0];
                if (m[1].indexOf('/') === 0) {
                    stack.pop();
                } else if (m[1].lastIndexOf('/') !== m[1].length - 1) {
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
} 