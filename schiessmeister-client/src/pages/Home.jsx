import { Link, useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { getCompetitions } from '../api/apiClient';
import { useAuth } from '../context/AuthContext';
import '../styles/Home.css';

const Home = () => {
	const [competitions, setCompetitions] = useState([]);
	const [error, setError] = useState(null);
	const navigate = useNavigate();
	const auth = useAuth();

	useEffect(() => {
		const fetchCompetitions = async () => {
			try {
				const data = await getCompetitions(auth);
				setCompetitions(data);
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
		</main>
	);
};

export default Home;
