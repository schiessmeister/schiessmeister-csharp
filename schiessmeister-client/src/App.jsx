import Login from "./pages/Login"
import Register from "./pages/Register"
import Home from "./pages/Home"
import ParticipantsList from "./pages/Participantslist"
import CreateCompetition from "./pages/CreateCompetition"
import { BrowserRouter, Routes, Route } from "react-router-dom";
import './App.css'
import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
          <Route path="login" element={<Login />} />
          <Route path="register" element={<Register />} />
          <Route path="home" element={<Home />} />
          <Route path="createcompetition" element={<CreateCompetition />} />
          <Route path="participantsList" element={<ParticipantsList />} />
      </Routes>
    </BrowserRouter>
  )
}

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <App />
  </StrictMode>
);
