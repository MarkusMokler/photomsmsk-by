module App.Collections {
    import PhotographerModel = App.PhotographerModel;

    export class PhotographersCollection extends Backbone.Collection<PhotographerModel>{
        constructor(model?) {
            this.url = App.makeUrl("/Api/Photographer");
            super(model);
        }

        url: string;

        initialize(options) {
        }
    }
}
