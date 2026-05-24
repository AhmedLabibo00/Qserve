# Voice Composition System

Implemented modular announcement engine with local audio fragments and fallback behavior.

## Supported formats
- mp3
- wav
- ogg

## Folders
- `Voices/`
- `Voices/Bell/`
- `Voices/Digits/`
- `Voices/Phrases/`

## Composition order
1. Bell
2. Customer phrase
3. Queue digits (prefix letters ignored)
4. Counter phrase
5. Counter digits

## Modes
- Bell + Customer + Counter
- Customer + Counter
- Bell + Customer
- Customer Only
- Bell Only
- Bell + Customer Without Counter

## Fallback rules
- Missing digit => warning, continue.
- Missing phrase => warning, continue.
- Missing bell => warning, continue.
- No crash behavior via warning-based composition.

## Cache
- `AudioFragmentCache` preloads/reuses local fragment paths to minimize queue-call delay.
