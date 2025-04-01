import { createApi } from '../utils/api';

export const getCompetitions = async (auth) => {
	if (!auth.userId) throw new Error('User not authenticated');

	const api = createApi(auth.token, auth.handleUnauthorized);
	return api.get(`/users/${auth.userId}/competitions`);
};

export const getCompetition = async (id, auth) => {
	const api = createApi(auth.token, auth.handleUnauthorized);
	return api.get(`/competition/${id}`);
};

export const updateCompetition = async (id, competitionData, auth) => {
	const api = createApi(auth.token, auth.handleUnauthorized);

	competitionData = {
		...competitionData,
		participations: competitionData.participations.map((p) => ({
			class: p.class,
			results: p.results,
			orderNb: p.orderNb,
			shooterId: p.shooterId
		}))
	};

	return api.put(`/competition/${id}`, competitionData);
};

export const getShooters = async (auth) => {
	const api = createApi(auth.token, auth.handleUnauthorized);
	return api.get('/shooter');
};

export const createShooter = async (name, auth) => {
	const api = createApi(auth.token, auth.handleUnauthorized);
	return api.post('/shooter', { name });
};
