import Login from './pages/Login';
import Register from './pages/Register';
import Home from './pages/Home';
import ParticipantsList from './pages/Participantslist';
import CreateCompetition from './pages/CreateCompetition';
import CompetitionOverview from './pages/CompetitionOverview';
import ResultsInput from './pages/ResultsInput';
import Logout from './pages/Logout';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import './App.css';
import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { AuthProvider } from './context/AuthContext';
import ProtectedRoute from './components/ProtectedRoute';

export default function App() {
	return (
		<BrowserRouter>
			<AuthProvider>
				<Routes>
					{/* Public routes */}
					<Route path="login" element={<Login />} />
					<Route path="register" element={<Register />} />
					<Route path="logout" element={<Logout />} />

					{/* Protected routes */}
					<Route element={<ProtectedRoute />}>
						<Route path="home" element={<Home />} />
						<Route path="createcompetition" element={<CreateCompetition />} />
						<Route path="participantsList/:id" element={<ParticipantsList />} />
						<Route path="competition/:id" element={<CompetitionOverview />} />
						<Route path="results/:competitionId/:participationId" element={<ResultsInput />} />
					</Route>

					{/* Redirects */}
					<Route path="/" element={<Navigate to="/home" replace />} />
					<Route path="*" element={<Navigate to="/home" replace />} />
				</Routes>
			</AuthProvider>
		</BrowserRouter>
	);
}

createRoot(document.getElementById('root')).render(
	<StrictMode>
		<App />
	</StrictMode>
);
