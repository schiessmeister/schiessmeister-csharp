import { Link, useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { getCompetitions, deleteUser } from '../api/apiClient';
import { useAuth } from '../context/AuthContext';
import '../styles/Home.css';
import { Button } from "@/components/ui/button"

const Home = () => {
	const [competitions, setCompetitions] = useState([]);
	const [error, setError] = useState(null);
	const navigate = useNavigate();
	const auth = useAuth();

	useEffect(() => {
		const fetchCompetitions = async () => {
			try {
				//const data = await getCompetitions(auth);
				//setCompetitions(data);
			} catch (err) {
				setError('Failed to load competitions');
				console.error(err);
			}
		};

		fetchCompetitions();
	}, [auth]);

	const handleCompetitionClick = (id) => {
		navigate(`/competition/${id}`);
	};

	const handleDeleteAccount = async () => {
		if (window.confirm('Möchten Sie Ihr Konto wirklich löschen? Diese Aktion kann nicht rückgängig gemacht werden.')) {
			try {
				await deleteUser(auth.userId, auth);
				auth.logout();
			} catch (err) {
				setError('Fehler beim Löschen des Kontos');
				console.error(err);
			}
		}
	};

	if (error) return <div className="error">{error}</div>;

	return (
		<main>
			<h2>Wettbewerb öffnen</h2>

			<div className="comp-list">
				{competitions.map((comp) => (
					<button className="button" key={comp.id} onClick={() => handleCompetitionClick(comp.id)}>
						{comp.name}
					</button>
				))}

				{competitions.length == 0 && <p>Noch keine Bewerbe.</p>}
			</div>
			
			<h2>Wettbewerb erstellen</h2>

			<Link to="/createcompetition">
				<button className="button">Erstellen</button>
			</Link>

			<div className="account-actions">
				<button className="button button--secondary logout-button" onClick={auth.logout}>
					Abmelden
				</button>
				<button className="button button--secondary delete-button" onClick={handleDeleteAccount}>
					Konto löschen
				</button>
			</div>
		</main>
	);
};

export default Home;
