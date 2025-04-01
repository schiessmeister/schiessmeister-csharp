import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getCompetition, updateCompetition } from '../api/apiClient';
import { useAuth } from '../context/AuthContext';
import '../styles/ResultsInput.css';

const SHOOTING_CLASSES = [
	{ key: 'MEN', value: 'Männer' },
	{ key: 'WOMEN', value: 'Frauen' },
	{ key: 'SENIORS', value: 'Senioren' }
];

const ResultsInput = () => {
	const { competitionId, participationId } = useParams();
	const navigate = useNavigate();
	const [competition, setCompetition] = useState(null);
	const [participation, setParticipation] = useState(null);
	const [results, setResults] = useState([]);
	const [error, setError] = useState(null);
	const [shootingClass, setShootingClass] = useState('MEN');
	const auth = useAuth();

	useEffect(() => {
		const fetchCompetition = async () => {
			try {
				const data = await getCompetition(competitionId, auth);
				setCompetition(data);
				const participation = data.participations.find((p) => p.id === parseInt(participationId));
				setParticipation(participation);
				if (participation.class) {
					setShootingClass(participation.class);
				}

				let tempResults = JSON.parse(participation.results || '[]');
				if (!Array.isArray(tempResults)) tempResults = [];
				setResults(tempResults);
			} catch (err) {
				setError('Failed to load competition details');
				console.error(err);
			}
		};

		fetchCompetition();
	}, [competitionId, participationId, auth]);

	const handleNumberClick = (number) => {
		if (results.length < 5) {
			setResults([...results, number]);
		}
	};

	const handleRemoveLast = () => {
		setResults(results.slice(0, -1));
	};

	const handleSave = async () => {
		try {
			const updatedParticipation = {
				...participation,
				results: JSON.stringify(results),
				class: shootingClass
			};

			const updatedParticipations = competition.participations.map((p) => (p.id === parseInt(participationId) ? updatedParticipation : p));

			const updatedCompetition = {
				...competition,
				participations: updatedParticipations
			};
			await updateCompetition(competitionId, updatedCompetition, auth);
			navigate(`/competition/${competitionId}`);
		} catch (err) {
			setError('Failed to save results');
			console.error(err);
		}
	};

	if (error) return <div className="error">{error}</div>;
	if (!participation) return <div>Loading...</div>;

	const isMaxResults = results.length >= 5;

	return (
		<main className="results-input">
			<h2>Ergebnisse für {participation.shooter.name}</h2>

			<div className="results-display">
				{results.map((result, index) => (
					<span key={index} className="result-number">
						{result}
					</span>
				))}
				{!isMaxResults && <span className="result-number result-number--empty">_</span>}
			</div>

			<div className="number-pad">
				<div className="number-grid">
					{[1, 2, 3, 4, 5, 6, 7, 8, 9, 0].map((num) => (
						<button key={num} className="number-button" onClick={() => handleNumberClick(num)} disabled={isMaxResults}>
							{num}
						</button>
					))}
					<button className="number-button number-button--large" onClick={() => handleNumberClick(10)} disabled={isMaxResults}>
						10
					</button>
				</div>
				<button className="button button--danger" onClick={handleRemoveLast} disabled={results.length === 0}>
					Letzte Zahl löschen
				</button>

				<div className="shooting-class-select">
					<label htmlFor="shootingClass">Schützenklasse:</label>
					<select id="shootingClass" value={shootingClass} onChange={(e) => setShootingClass(e.target.value)}>
						{SHOOTING_CLASSES.map(({ key, value }) => (
							<option key={key} value={key}>
								{value}
							</option>
						))}
					</select>
				</div>
			</div>

			<div className="action-buttons">
				<button className="button button--secondary reset-btn" onClick={() => navigate(`/competition/${competitionId}`)}>
					Abbrechen
				</button>
				<button className="button" onClick={handleSave}>
					Speichern
				</button>
			</div>
		</main>
	);
};

export default ResultsInput;
