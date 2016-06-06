module App.Routers {

    export class PhotomskRouter extends Backbone.Router {
        routes: any;
        constructor(options?: Backbone.RouterOptions) {

            this.routes = {
                "": "mainWall", // Пустой hash-тэг
                "/": "mainWall", // Начальная страница
                "Home": "mainWall"
            }


            super(options);
        }

        mainWall() {
            App.currentView = new App.Views.MainPage();
            App.currentView.render();
        }
    }
}