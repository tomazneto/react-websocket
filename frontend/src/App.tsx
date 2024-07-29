import React, { useEffect, useState } from 'react';
import './App.css';
import Connector from './signalRConnection';
import Home from './Home';

function App() {
  const { newMessage, events } = Connector();
  const [message, setMessage] = useState("initial value");

  useEffect(() => {
    events((message) => setMessage(message));
  });

  return (
    <>    
    <div className="App">
      <span>message from signalR: <span style={{ color: "green" }}>{message}</span> </span>
    </div>
    <br />
    </>

  );
}
export default App;