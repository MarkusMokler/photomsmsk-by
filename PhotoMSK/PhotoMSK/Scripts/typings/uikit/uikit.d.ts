/// <reference path="../jquery/jquery.d.ts"/>

interface ModalStatic {
    new (string): JQuery;
}

interface UIKitStatic {
    notify(message);
    notify(message, any);
    uploadSelect(elem: any, setting: any): void;
    uploadDrop(elem: any, setting: any): void;

    modal:ModalStatic;
}
declare module "uikit" {
    export = UIkit;
}
declare var UIkit: UIKitStatic;
