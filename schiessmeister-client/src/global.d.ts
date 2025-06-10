declare module 'react' {
  export const useState: any
  export const useEffect: any
  export const useContext: any
  export const createContext: any
  export const forwardRef: any
  export namespace React {
    type FC<P = any> = (props: P) => any
  }
  export type ComponentProps<T extends any> = any
  export type ComponentPropsWithoutRef<T extends any> = any
  export type ElementRef<T extends any> = any
  const React: any
  export default React
}
declare module 'react-dom/client' {
  export const createRoot: any
}
declare module 'react-router-dom'
declare module '@radix-ui/react-slot'
declare module 'class-variance-authority'
declare module 'clsx'
declare module 'tailwind-merge'
declare module '@microsoft/signalr'
declare module 'js-confetti'

declare module 'react/jsx-runtime' {
  export const jsx: any
  export const jsxs: any
  export const Fragment: any
}

declare namespace JSX {
  interface IntrinsicElements {
    [elemName: string]: any
  }
}
