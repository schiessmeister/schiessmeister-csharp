import { useParams, useNavigate } from 'react-router-dom';
import { useData } from '../context/DataContext';
import CreateCompetitionForm from './CreateCompetitionForm';

const EditCompetition: React.FC = () => {
  const { id } = useParams();
  const { competitions, updateCompetition } = useData();
  const navigate = useNavigate();

  const competition = competitions.find((c) => c.id === parseInt(id));

  if (!competition) return <div>Wettbewerb nicht gefunden</div>;

  const handleSave = (comp: Record<string, string>) => {
    updateCompetition(competition.id, comp);
    navigate(`/competitions/${competition.id}`);
  };

  return (
    <main>
      <h2>Wettbewerb bearbeiten</h2>
      <CreateCompetitionForm onSave={handleSave} initial={competition} />
    </main>
  );
};

export default EditCompetition;
