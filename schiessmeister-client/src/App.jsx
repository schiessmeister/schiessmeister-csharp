import Login from './pages/Login';
import Register from './pages/Register';
import Competitions from './pages/Competitions';
import CreateCompetition from './pages/CreateCompetition';
import EditCompetition from './pages/EditCompetition';
import CompetitionDetail from './pages/CompetitionDetail';
import Results from './pages/Results';
import ResultsInput from './pages/ResultsInput';
import CompetitionOverview from './pages/CompetitionOverview';
import ParticipantsList from './pages/Participantslist';
import CreateParticipantGroup from './pages/CreateParticipantGroup';
import EditParticipantGroup from './pages/EditParticipantGroup';
import Logout from './pages/Logout';
import CompetitionLeaderboard from './pages/CompetitionLeaderboard';
import PublicLeaderboard from './pages/PublicLeaderboard';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import './App.css';
import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { AuthProvider } from './context/AuthContext';
import { DataProvider } from './context/DataContext';
import ProtectedRoute from './components/ProtectedRoute';
import { NavigationMenu, NavigationMenuList, NavigationMenuItem, NavigationMenuLink } from '@/components/ui/navigation-menu';
import { Shield } from 'lucide-react';

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
                                                <Route path="public-leaderboard/:id" element={<PublicLeaderboard />} />

                                                {/* Protected routes */}
                                                <Route element={<ProtectedRoute />}>
                                                        {/* Manager */}
                                                        <Route path="manager/competitions" element={<Competitions />} />
                                                        <Route path="manager/competitions/new" element={<CreateCompetition />} />
                                                        <Route path="manager/competitions/:id" element={<CompetitionDetail editable={false} />} />
                                                        <Route path="manager/competitions/:id/edit" element={<EditCompetition />} />
                                                        <Route path="manager/competitions/:id/leaderboard" element={<CompetitionLeaderboard />} />
                                                        <Route path="manager/participant-groups/new" element={<CreateParticipantGroup />} />
                                                        <Route path="manager/participant-groups/:id/edit" element={<EditParticipantGroup />} />

                                                        {/* Writer */}
                                                        <Route path="writer/competitions" element={<CompetitionOverview />} />
                                                        <Route path="writer/competitions/:id" element={<CompetitionOverview />} />
                                                        <Route path="writer/results/:competitionId/:participationId" element={<ResultsInput />} />
                                                        <Route path="writer/participantsList/:id" element={<ParticipantsList />} />
                                                </Route>

                                                {/* Redirects */}
                                                <Route path="/" element={<Navigate to="/login" replace />} />
                                                <Route path="*" element={<Navigate to="/login" replace />} />
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
