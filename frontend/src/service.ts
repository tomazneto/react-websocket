import axios from "axios"
import Response from "./Response";

export default class Service {
    private static baseUrl: string = "https://localhost:63383/WeatherForecast";


    public static create<T>(url: string, obj: T): Promise<Response> {

        let res = axios.post(this.baseUrl + url, obj)
            .then(response => {
                const result = response.data;
                if (result && result.success) {
                    return new Response(true, result.data, "Sucesso", "");
                } else {
                    return new Response(false, null, "Erro", "");
                }
            }).catch(function (error) {
                return new Response(false, null, "Erro", error);
            });

        return res;
    }

}