
module App.Models {

    export class WallModel extends Backbone.Model {
        constructor(model?) {
            this.url = App.makeUrl("/Api/Wall");
            super(model);
        }

        url: string;

        defaults() {
            return {
                title: ""
            };
        }
        parse(data) {
            var response = {};
            return response;
        }
    };
}

