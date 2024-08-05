export default class Response{
    public Status: boolean;
    public Data: any;
    public Messages: string;
    public Exception: string;

    constructor(status: boolean, data: any, msg: string, exception: string){
        this.Status = status;
        this.Data = data;
        this.Messages = msg;
        this.Exception = exception;
    }   
}