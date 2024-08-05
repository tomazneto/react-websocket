import React, { useEffect, useState } from 'react';
import './App.css';
import { Button } from '@mui/material';
import Connector from './signalRConnection';
import { Valor } from "./Valor";
import { Bar } from 'react-chartjs-2';
import Service from './service';

import {
  BarElement,
  CategoryScale,
  Chart as ChartJS,
  Legend,
  LinearScale,
  Title,
  Tooltip,
} from 'chart.js';

import { ThemeProvider } from "@mui/material";
import {
  BNBTheme,
  AccordionBNB,
  Header,
  MenuBNB,
  MenuItemBNB,
  Container,
  TextField
} from "bnb-ui";
import { text } from 'stream/consumers';
import RequestValor from './RequestValor';

ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);

export const options = {
  plugins: {
    title: {
      display: true,
      text: 'Requisições Logística',
    },
  },
  responsive: true,
  scales: {
    x: {
      stacked: true,
    },
    y: {
      stacked: true,
    },
  },
};

function App() {

  const { dados, events } = Connector();
  const [valores, setValores] = useState<Valor[]>([]);
  const [ano, setAno] = useState("");
  const [order, setOrder] = useState("");

  const dataset = {
    labels: valores.map((x) => x.ano),
    datasets: [
      {
        label: 'PCA',
        backgroundColor: '#f87979',
        data: valores.map((x) => x.orderid),
      },
    ],
  };

  useEffect(() => {
    events((valores) => setValores(valores));
  });

  const handleSubmit = (event: any) => {
    event.preventDefault();
    Service.create<RequestValor>("", new RequestValor(Number(ano), Number(order)));
  }

  return (
    <div className="App">
      <ThemeProvider theme={BNBTheme}>
        <Header version={"0.1.0"} system={"POC - react + rest api + signalr + websocket + kafka"}>
          <MenuBNB
            menuTitle={"menu teste"}
            iconMenuOpen={false}
          >
            <MenuItemBNB primary={"item 1"} onClick={() => console.log('Insira neste onClick a função de navegação')} />
          </MenuBNB>
        </Header>
        <Container>
          <AccordionBNB title={"Cadastrar Item"}>
            <br></br>
            <form onSubmit={handleSubmit}>
              <div style={{ display: ' flex', columnGap: '15px' }}>
                <TextField
                  id="ano"
                  label="Ano das Requisições"
                  onChange={(e) => setAno(e.target.value)}
                  fullWidth
                  required
                />
                <TextField
                  id="qtd"
                  label="Quantidade de Requisições"
                  onChange={(e) => setOrder(e.target.value)}
                  fullWidth
                  required
                />

              </div>

              <br></br>
              <div style={{ float: 'right' }}>
                <Button variant="contained" type='submit'>Salvar</Button>
              </div>
            </form>
          </AccordionBNB>
          <AccordionBNB title={"Gráfico de itens Cadastrados por periodo"}>
            <Bar options={options} data={dataset} />
          </AccordionBNB>
        </Container>
      </ThemeProvider>
    </div>
  );
}

export default App;
