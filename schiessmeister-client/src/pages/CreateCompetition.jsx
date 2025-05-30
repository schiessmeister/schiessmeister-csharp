import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { createApi } from '../utils/api';

const CreateCompetition = () => {
	const navigate = useNavigate();
	const { userId, token, handleUnauthorized } = useAuth();
	const [name, setName] = useState('');
	const [location, setLocation] = useState('');
	const [date, setDate] = useState('');

	const handleSubmit = async (e) => {
		e.preventDefault();

		try {
			const api = createApi(token, handleUnauthorized);
			await api.post('/competition', {
				name,
				date,
				location,
				organizerId: parseInt(userId)
			});

			navigate('/home');
		} catch (error) {
			console.error('Failed to create competition:', error);
		}
	};

	const handleReset = () => {
		navigate(-1);
	};

	return (
		<main>
			<h2>Wettbewerb erstellen</h2>
			<form onSubmit={handleSubmit}>
				<input type="text" placeholder="Name" value={name} onChange={(e) => setName(e.target.value)} required />

				<input type="text" placeholder="Standort" value={location} onChange={(e) => setLocation(e.target.value)} required />

				<div>Datum</div>
				<input type="datetime-local" value={date} onChange={(e) => setDate(e.target.value)} required />

				<button className="button" type="submit">
					Speichern
				</button>

				<button type="button" className="button button--secondary" onClick={handleReset}>
					Abbrechen
				</button>
			</form>
		</main>
	);
};

export default CreateCompetition;
