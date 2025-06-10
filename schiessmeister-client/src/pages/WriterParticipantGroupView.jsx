import { useParams, useNavigate } from 'react-router-dom';
import { sampleCompetitions } from '../data/sampleData';
import { Button } from '@/components/ui/button';

const WriterParticipantGroupView = () => {
  const { competitionId, groupId } = useParams();
  const navigate = useNavigate();
  const competition = sampleCompetitions.find(c => String(c.id) === String(competitionId));
  const group = competition?.participantGroups?.find(g => String(g.id) === String(groupId));

  if (!competition || !group) return <div>Gruppe nicht gefunden</div>;

  return (
    <main className="max-w-2xl mx-auto mt-8">
      <h2 className="text-2xl font-bold mb-6">{competition.name} – {group.title}</h2>
      <ul className="space-y-2">
        {group.participations.map(p => (
          <li key={p.id} className="flex items-center gap-4 p-2 bg-gray-50 rounded">
            <span className="font-mono w-8">{p.id}</span>
            <span className="font-medium">{p.shooter?.name}</span>
            <span className="text-xs text-muted-foreground">{p.shooter?.email}</span>
            <span className="ml-auto text-sm">{p.discipline}</span>
            <span className="ml-4 text-sm">{p.team}</span>
            <Button variant="outline" onClick={() => navigate(`/writer/results/${competitionId}/${p.id}`)}>
              Ergebnisse eintragen
            </Button>
          </li>
        ))}
        {group.participations.length === 0 && <li className="text-muted-foreground">Keine Teilnehmer</li>}
      </ul>
      <div className="mt-8">
        <Button variant="secondary" onClick={() => navigate(`/writer/competitions`)}>
          Zurück
        </Button>
      </div>
    </main>
  );
};

export default WriterParticipantGroupView; 