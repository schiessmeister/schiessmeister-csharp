import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { createApi } from '../utils/api';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';

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
                                <Input type="text" placeholder="Name" value={name} onChange={(e) => setName(e.target.value)} required />

                                <Input type="text" placeholder="Standort" value={location} onChange={(e) => setLocation(e.target.value)} required />

                                <div>Datum</div>
                                <Input type="datetime-local" value={date} onChange={(e) => setDate(e.target.value)} required />

                                <Button type="submit">Speichern</Button>

                                <Button type="button" variant="secondary" onClick={handleReset}>
                                        Abbrechen
                                </Button>
                        </form>
		</main>
	);
};

export default CreateCompetition;
