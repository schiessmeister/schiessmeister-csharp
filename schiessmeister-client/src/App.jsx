import Login from './pages/Login';
import Register from './pages/Register';
import Competitions from './pages/Competitions';
import CreateCompetition from './pages/CreateCompetition';
import EditCompetition from './pages/EditCompetition';
import CompetitionDetail from './pages/CompetitionDetail';
import Results from './pages/Results';
import CreateParticipantGroup from './pages/CreateParticipantGroup';
import EditParticipantGroup from './pages/EditParticipantGroup';
import Logout from './pages/Logout';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import './App.css';
import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { AuthProvider } from './context/AuthContext';
import { DataProvider } from './context/DataContext';
import ProtectedRoute from './components/ProtectedRoute';

export default function App() {
        return (
                <BrowserRouter>
                        <AuthProvider>
                                <DataProvider>
                                        <Routes>
                                                {/* Public routes */}
                                                <Route path="login" element={<Login />} />
                                                <Route path="register" element={<Register />} />
                                                <Route path="logout" element={<Logout />} />

                                                {/* Protected routes */}
                                                <Route element={<ProtectedRoute />}>
                                                        <Route path="competitions" element={<Competitions />} />
                                                        <Route path="competitions/new" element={<CreateCompetition />} />
                                                        <Route path="competitions/:id" element={<CompetitionDetail />} />
                                                        <Route path="competitions/:id/edit" element={<EditCompetition />} />
                                                        <Route path="results" element={<Results />} />
                                                        <Route path="participant-groups/new" element={<CreateParticipantGroup />} />
                                                        <Route path="participant-groups/:id/edit" element={<EditParticipantGroup />} />
                                                </Route>

                                                {/* Redirects */}
                                                <Route path="/" element={<Navigate to="/competitions" replace />} />
                                                <Route path="*" element={<Navigate to="/competitions" replace />} />
                                        </Routes>
                                </DataProvider>
                        </AuthProvider>
                </BrowserRouter>
        );
}

createRoot(document.getElementById('root')).render(
	<StrictMode>
		<App />
	</StrictMode>
);
