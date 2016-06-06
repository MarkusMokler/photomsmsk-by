module App.Collections {
    import PhototechnicsModel = Models.PhototechnicsModel;

    export class PhototechnicsCollection extends Backbone.Collection<PhototechnicsModel>{
        constructor(model?) {
            this.url = App.makeUrl("/Api/PhototechnicsViewModel");
            super(model);
        }

        url: string;

        initialize(options) {
        }
    }
}