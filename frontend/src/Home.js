import React, { useEffect, useState } from 'react';
import './App.css';
import Connector from './signalRConnection';

function Home() {
  const { newMessage, events } = Connector();
  const [message, setMessage] = useState("initial value");

  useEffect(() => {
    events((message) => setMessage(message));
  });

  return (
    <div className="App">
      <span>message from signalR Home: <span style={{ color: "green" }}>{message}</span> </span>
    </div>
  );
}
export default Home;