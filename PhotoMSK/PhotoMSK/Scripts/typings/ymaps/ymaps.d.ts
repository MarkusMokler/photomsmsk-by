interface MapStatic {
    new (string, any): any;
}

interface YMapsStatic {
    ready(any);
    Map: MapStatic;
    Placemark: any;
}
declare module "ymaps" {
    export = ymaps;
}
declare var ymaps: YMapsStatic;
 