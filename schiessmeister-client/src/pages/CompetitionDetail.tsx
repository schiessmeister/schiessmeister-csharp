import { Link, useParams } from 'react-router-dom';
import { useData } from '../context/DataContext';
import { Button } from '@/components/ui/button';

const CompetitionDetail: React.FC = () => {
  const { id } = useParams();
  const { competitions } = useData();
  const competition = competitions.find((c) => c.id === parseInt(id));

  if (!competition) return <div>Wettbewerb nicht gefunden</div>;

  return (
    <main>
      <h2>{competition.name}</h2>
      <p>
        <strong>Ort:</strong> {competition.location}
      </p>
      <p>
        <strong>Datum:</strong> {competition.date}
      </p>

      <Link to={`/competitions/${id}/edit`}>
        <Button>Bearbeiten</Button>
      </Link>
    </main>
  );
};

export default CompetitionDetail;
