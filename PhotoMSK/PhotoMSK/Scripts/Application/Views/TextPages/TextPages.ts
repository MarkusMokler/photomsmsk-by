///<reference path="../../SharedViews/BaseViews/AbstractView.ts"/>
///<reference path="../MenuItem/MenuItem.ts"/>
///<reference path="../../Application.ts"/>

module App.Views {
 
    export class TextPageModel extends Backbone.Model {

    };

    export class TextPagesEditView extends App.BaseViews.BaseEditView<TextPageModel>{
        constructor(model?) {
            this.el = "body";
            this.events = {
                "click [data-role-save-button]": "save",
                "click [data-role-new-category]": "newCategory",
                "click [data-role-save-category]": "saveCategory",
                "change [data-role-input]": "changeInput",
                "click [data-role-save-shortcut]": "saveShortcut"
            };
            super(model);
        }

        categories: CollectionView;
        categoryModel: PageCategoryModel;

    initialize (options) {
        var self = this;
        self.model = new TextPageModel({ routeId: App.owner });
        this.listenTo(this.model, "sync", this.success);
        this.listenTo(this.model, "error", this.error);
    }
    render () {
        this.categories = new Views.CollectionView({ routeId: App.owner });
        $('[data-role-select-placeholder]').append(this.categories.render().el);
        this.listenTo(this.categories, "CategoryChanged", this.categoryChanged);
        return this;
    }
    categoryChanged (arg) {
        this.model.set("pageCategoryId", arg);
    }
    newCategory () {
        this.categoryModel = new PageCategoryModel({ routeID: App.owner });
        this.categoryModel.on("sync", this.categorySaved, this);
    }
    saveCategory () {
        var vv = $("[data-role-category-name]").val();
        this.categoryModel.set("categoryName", vv);
        this.categoryModel.save();
    }
    categorySaved (a, b, c) {
        this.categories.collection.add(this.categoryModel);
    }
    saveShortcut () {
        this.model.set("slug", $("[data-role-shortcut]").val());
    }
    save () {
        //   var text = $('.CodeMirror')[0].CodeMirror.getValue();
        // this.model.set("text", text);
        //  this.model.save();
    }
}
}
