import { createApi } from '../utils/api';

export const loginRequest = async (username, password) => {
    // MOCK: Immer Erfolg, egal welche Daten
    return Promise.resolve({ token: 'mock-token', id: 1 });
};

export const registerRequest = async (username, email, password) => {
    const api = createApi();
    return api.post('/authenticate/register', { username, email, password });
};

export const getSubscriptionDetails = async (competitionId) => {
    const api = createApi();
    return api.get(`/competition/${competitionId}/subscribe`);
};
