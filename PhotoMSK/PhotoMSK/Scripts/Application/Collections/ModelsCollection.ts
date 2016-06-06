module App.Models {
    export class EventModel extends Backbone.Model {
        constructor(model?) {
            this.urlRoot = App.makeUrl("/Api/Events");
            super(model);
        }

        urlRoot: string;
    }
}

module App.Collections {
    import EventModel = Models.EventModel;
    import PhotoModel = App.PhotoModelModel;

    export class EventCollection extends Backbone.Collection<EventModel> {
        constructor(model?) {
            this.url = App.makeUrl("/Api/Events");
            super(model);
        }

        url: string;

        initialize(options) {
        }
    }

    export class ModelsCollection extends Backbone.Collection<PhotoModel> {
        constructor(model?) {
            this.url = App.makeUrl("/Api/Photomodels");
            super(model);
        }

        url: string;

        initialize(options) {
        }
    }

}