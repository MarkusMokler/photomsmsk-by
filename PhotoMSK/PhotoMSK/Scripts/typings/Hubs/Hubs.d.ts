// Get signalr.d.ts.ts from https://github.com/borisyankov/DefinitelyTyped (or delete the reference)
/// <reference path="../signalr/signalr.d.ts" />
/// <reference path="../jquery/jquery.d.ts" />

////////////////////
// available hubs //
////////////////////
//#region available hubs

interface SignalR {

    /**
      * The hub implemented by PhotoMSK.Hubs.CallerHub
      */
    callerHub : CallerHub;

    /**
      * The hub implemented by PhotoMSK.Hubs.CardHub
      */
    cardHub : CardHub;

    /**
      * The hub implemented by PhotoMSK.Hubs.MessageHub
      */
    messageHub : MessageHub;
}
//#endregion available hubs

///////////////////////
// Service Contracts //
///////////////////////
//#region service contracts

//#region CallerHub hub

interface CallerHub {
    
    /**
      * This property lets you send messages to the CallerHub hub.
      */
    server : CallerHubServer;

    /**
      * The functions on this property should be replaced if you want to receive messages from the CallerHub hub.
      */
    client : any;
}

interface CallerHubServer {

    /** 
      * Sends a "hello" message to the CallerHub hub.
      * Contract Documentation: ---
      * @return {JQueryPromise of void}
      */
    hello() : JQueryPromise<void>
}

//#endregion CallerHub hub


//#region CardHub hub

interface CardHub {
    
    /**
      * This property lets you send messages to the CardHub hub.
      */
    server : CardHubServer;

    /**
      * The functions on this property should be replaced if you want to receive messages from the CardHub hub.
      */
    client : any;
}

interface CardHubServer {

    /** 
      * Sends a "send" message to the CardHub hub.
      * Contract Documentation: ---
      * @param id {string} 
      * @param info {string} 
      * @return {JQueryPromise of void}
      */
    send(id : string, info : string) : JQueryPromise<void>
}

//#endregion CardHub hub


//#region MessageHub hub

interface MessageHub {
    
    /**
      * This property lets you send messages to the MessageHub hub.
      */
    server : MessageHubServer;

    /**
      * The functions on this property should be replaced if you want to receive messages from the MessageHub hub.
      */
    client : any;
}

interface MessageHubServer {

    /** 
      * Sends a "send" message to the MessageHub hub.
      * Contract Documentation: ---
      * @param id {string} 
      * @param message {string} 
      * @return {JQueryPromise of void}
      */
    send(id : string, message : string) : JQueryPromise<void>
}

//#endregion MessageHub hub

//#endregion service contracts



////////////////////
// Data Contracts //
////////////////////
//#region data contracts

//#endregion data contracts

