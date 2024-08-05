import * as signalR from "@microsoft/signalr";
import { Valor } from "./Valor";
const URL = process.env.HUB_ADDRESS ?? "https://localhost:63383/hub"; //or whatever your backend port is

class Connector {
    private connection: signalR.HubConnection;
    public events: (onMessageReceived: (message: Valor[]) => void) => void;
    static instance: Connector;
    public dados: Valor[] = [];

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
                this.dados = message;
                console.log(message);
            });
        };
    }

    public static getInstance(): Connector {
        if (!Connector.instance)
            Connector.instance = new Connector();
        return Connector.instance;
    }

}
export default Connector.getInstance;