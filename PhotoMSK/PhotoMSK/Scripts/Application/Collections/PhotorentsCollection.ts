module App.Collections {
    import PhotorentModel = App.Models.PhotorentModel;

    export class PhotorentsCollection extends Backbone.Collection<PhotorentModel>{
        constructor(model?) {
            this.url = App.makeUrl("/Api/Photorents");
            super(model);
        }

        url: string;

        initialize(options) {
        }
    }
}