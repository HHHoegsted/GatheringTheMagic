import requests
import pyodbc
import time

# Database connection
conn = pyodbc.connect(
    'DRIVER={ODBC Driver 17 for SQL Server};'
    'SERVER=localhost\\SQLEXPRESS;'
    'DATABASE=MTGCards;'
    'Trusted_Connection=yes;'
)
cursor = conn.cursor()

def insert_card(card):
    # Insert into Cards table
    cursor.execute("""
        IF NOT EXISTS (SELECT 1 FROM Cards WHERE id = ?)
        INSERT INTO Cards (id, name, manaCost, cmc, type, rarity, setCode, setName, text, power, toughness, layout, number, artist, imageUrl, originalText, originalType, multiverseid)
        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
    """, card.get("id"),
         card.get("id"),
         card.get("name"),
         card.get("manaCost"),
         card.get("cmc"),
         card.get("type"),
         card.get("rarity"),
         card.get("set"),
         card.get("setName"),
         card.get("text"),
         card.get("power"),
         card.get("toughness"),
         card.get("layout"),
         card.get("number"),
         card.get("artist"),
         card.get("imageUrl"),
         card.get("originalText"),
         card.get("originalType"),
         card.get("multiverseid"))

    # CardColors
    for color in card.get("colors", []):
        cursor.execute("INSERT INTO CardColors (card_id, color) VALUES (?, ?)", card["id"], color)

    # CardColorIdentity
    for color in card.get("colorIdentity", []):
        cursor.execute("INSERT INTO CardColorIdentity (card_id, color) VALUES (?, ?)", card["id"], color)

    # CardTypes
    for t in card.get("types", []):
        cursor.execute("INSERT INTO CardTypes (card_id, type) VALUES (?, ?)", card["id"], t)

    # CardSubtypes
    for st in card.get("subtypes", []):
        cursor.execute("INSERT INTO CardSubtypes (card_id, subtype) VALUES (?, ?)", card["id"], st)

    # CardPrintings
    for printing in card.get("printings", []):
        cursor.execute("INSERT INTO CardPrintings (card_id, setCode) VALUES (?, ?)", card["id"], printing)

    # ForeignNames
    for name in card.get("foreignNames", []):
        cursor.execute("""
            INSERT INTO ForeignNames (card_id, language, name, type, text, flavor, imageUrl, multiverseid)
            VALUES (?, ?, ?, ?, ?, ?, ?, ?)
        """, card["id"],
             name.get("language"),
             name.get("name"),
             name.get("type"),
             name.get("text"),
             name.get("flavor"),
             name.get("imageUrl"),
             name.get("multiverseid"))

    # Legalities
    for legal in card.get("legalities", []):
        cursor.execute("""
            INSERT INTO Legalities (card_id, format, legality)
            VALUES (?, ?, ?)
        """, card["id"], legal.get("format"), legal.get("legality"))

    conn.commit()

def fetch_cards():
    page = 1
    while page < 11:
        url = f"https://api.magicthegathering.io/v1/cards?page={page}&pageSize=50"
        print(f"Fetching page {page}...")
        response = requests.get(url)
        if response.status_code != 200:
            print(f"Error fetching cards: {response.status_code}")
            break

        cards = response.json().get("cards", [])
        if not cards:
            print("No more cards to fetch.")
            break

        for card in cards:
            try:
                if "★" not in card.get("number") or "s" not in card.get("number") or "p" not in card.get("number"):
                    insert_card(card)
            except Exception as e:
                print(f"Error inserting card {card.get('name')} ({card.get('id')}): {e}")

        page += 1
        time.sleep(5)

try:
    fetch_cards()
except KeyboardInterrupt:
    print("Process interrupted.")
finally:
    cursor.close()
    conn.close()
