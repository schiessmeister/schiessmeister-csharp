import { createApi } from '../utils/api'
import type { AuthContextType } from '../context/AuthContext'
import type { Competition } from '../types'

export const getCompetitions = async (auth: AuthContextType) => {
        if (!auth.userId) throw new Error('User not authenticated');

	const api = createApi(auth.token, auth.handleUnauthorized);
	return api.get(`/users/${auth.userId}/competitions`);
};

export const getCompetition = async (id: string | number, auth: AuthContextType | null = null) => {
	if (auth) {
		const api = createApi(auth.token, auth.handleUnauthorized);
		return api.get(`/competition/${id}`);
	}
	// For public access without auth
	const api = createApi();
	return api.get(`/competition/${id}`);
};

export const updateCompetition = async (
  id: string | number,
  competitionData: Competition,
  auth: AuthContextType
) => {
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

export const getShooters = async (auth: AuthContextType) => {
	const api = createApi(auth.token, auth.handleUnauthorized);
	return api.get('/shooter');
};

export const createShooter = async (name: string, auth: AuthContextType) => {
	const api = createApi(auth.token, auth.handleUnauthorized);
	return api.post('/shooter', { name });
};

export const deleteUser = async (userId: string | number, auth: AuthContextType) => {
	const api = createApi(auth.token, auth.handleUnauthorized);
	return api.delete(`/users/${userId}`);
};

export const deleteCompetition = async (id: string | number, auth: AuthContextType) => {
	const api = createApi(auth.token, auth.handleUnauthorized);
	return api.delete(`/competition/${id}`);
};
