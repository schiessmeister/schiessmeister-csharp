import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getCompetition, deleteCompetition } from '../api/apiClient';
import { useAuth } from '../context/AuthContext';
import '../styles/CompetitionOverview.css';

const CompetitionOverview = () => {
	const { id } = useParams();
	const navigate = useNavigate();
	const [competition, setCompetition] = useState(null);
	const [error, setError] = useState(null);
	const auth = useAuth();

	useEffect(() => {
		const fetchCompetition = async () => {
			try {
				const data = await getCompetition(id, auth);
				setCompetition(data);
			} catch (err) {
				setError('Failed to load competition details');
				console.error(err);
			}
		};

		fetchCompetition();
	}, [id, auth]);

	const handleDeleteCompetition = async () => {
		if (window.confirm('Möchten Sie diesen Wettbewerb wirklich löschen? Diese Aktion kann nicht rückgängig gemacht werden.')) {
			try {
				await deleteCompetition(id, auth);
				navigate('/');
			} catch (err) {
				setError('Fehler beim Löschen des Wettbewerbs');
				console.error(err);
			}
		}
	};

	if (error) return <div className="error">{error}</div>;
	if (!competition) return <div>Loading...</div>;

	return (
		<main>
			<h2>{competition.name}</h2>
			<div className="competition-details">
				<p>
					<strong>Datum:</strong> {new Date(competition.date).toLocaleString()}
				</p>
				<p>
					<strong>Ort:</strong> {competition.location}
				</p>
				<p>
					<strong>Teilnehmer:</strong> {competition.participations.length}
				</p>
			</div>
			<button className="button button--secondary" onClick={() => window.open(`/public-leaderboard/${id}`, '_blank')}>
				Live Rangliste
			</button>
			<div className="participants-list">
				<h3>Teilnehmer</h3>
				<div className="participants-grid">
					{competition.participations.map((participation) => (
						<div key={participation.id} className="participant-item">
							<span className="participant-name">{participation.shooter.name}</span>
							<button className="button button--tertiary results-btn" onClick={() => navigate(`/results/${id}/${participation.id}`)}>
								Ergebnisse
							</button>
						</div>
					))}

					{competition.participations.length == 0 && <p>Keine Teilnehmer.</p>}
				</div>
			</div>
			<button className="button" onClick={() => navigate(`/participantsList/${id}`)}>
				Teilnehmer verwalten
			</button>
			<div className="competition-actions">
				<button className="button button--secondary" onClick={() => navigate(`/`)}>
					Zurück
				</button>
				<button className="button button--secondary delete-button" onClick={handleDeleteCompetition}>
					Wettbewerb löschen
				</button>
			</div>
		</main>
	);
};

export default CompetitionOverview;
