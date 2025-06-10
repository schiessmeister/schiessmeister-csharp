import { createApi } from '../utils/api'

export const loginRequest = async (username: string, password: string) => {
    const api = createApi();
    return api.post('/authenticate/login', { username, password });
};

export const registerRequest = async (
  username: string,
  email: string,
  password: string
) => {
    const api = createApi();
    return api.post('/authenticate/register', { username, email, password });
};

export const getSubscriptionDetails = async (competitionId: number | string) => {
    const api = createApi();
    return api.get(`/competition/${competitionId}/subscribe`);
};
