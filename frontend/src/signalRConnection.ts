import * as signalR from "@microsoft/signalr";
const URL = process.env.HUB_ADDRESS ?? "https://localhost:32781/hub"; //or whatever your backend port is

class Connector {
    private connection: signalR.HubConnection;
    public events: (onMessageReceived: (message: string) => void) => void;
    static instance: Connector;

    constructor() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(URL)
            .withAutomaticReconnect()
            .build();
        this.connection.start().catch(err => {
            document.write(err);
            console.log('aqui: ' + err)
        });
        this.events = (onMessageReceived) => {
            this.connection.on("SendMessage", (message) => {
                onMessageReceived(message);
            });
        };
    }

    public newMessage = (messages: string) => {
        this.connection.send("SendMessage", messages).then(x => console.log("sent"))
    }

    public static getInstance(): Connector {
        if (!Connector.instance)
            Connector.instance = new Connector();
        return Connector.instance;
    }

}
export default Connector.getInstance;