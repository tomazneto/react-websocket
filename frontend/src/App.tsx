import React, { useEffect, useState } from 'react';
import './App.css';
import Connector from './signalRConnection';
import { Valor } from "./Valor";

import {
  BarElement,
  CategoryScale,
  Chart as ChartJS,
  Legend,
  LinearScale,
  Title,
  Tooltip,
} from 'chart.js';
import { Bar } from 'react-chartjs-2';

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
      text: 'Projeto Conceito',
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

  const dataset = {
    labels: valores.map((x)=> x.ano),
    datasets: [
      {
        label: 'GitHub Commits',
        backgroundColor: '#f87979',
        data: valores.map((x)=> x.orderid),
      },
    ],
  };

  useEffect(() => {
    events((valores) => setValores(valores));
  });

  return (
    <>
      <div className="App">
        <span>message from signalR: <span style={{ color: "green" }}></span> </span>
      </div>
      <br />
      <div className="grafico">
        <Bar options={options} data={dataset} />
      </div>
    </>

  );
}
export default App;