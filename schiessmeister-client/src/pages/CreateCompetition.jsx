import { useNavigate } from 'react-router-dom';
import { useData } from '../context/DataContext';
import CreateCompetitionForm from './CreateCompetitionForm';

const CreateCompetition = () => {
  const { addCompetition } = useData();
  const navigate = useNavigate();

  const handleSave = (comp) => {
    addCompetition(comp);
    navigate('/competitions');
  };

  return (
    <main>
      <h2>Neuer Wettbewerb</h2>
      <CreateCompetitionForm onSave={handleSave} />
    </main>
  );
};

export default CreateCompetition;
