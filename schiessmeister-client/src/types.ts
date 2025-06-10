export interface Competition {
  id: number
  name: string
  location: string
  date: string
  writers: unknown[]
  disciplines: unknown[]
  participantGroups: unknown[]
  participations?: any[]
}
